using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using VarAstroMasters.Shared;

namespace VarAstroMasters.Server.Services.LightCurveService;

public class LightCurveService : ILightCurveService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LightCurveService(DataContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<LightCurveDTO>>> LightCurveListGet()
    {
        var data = await _context.LightCurves
            .Where(lc => lc.PublishVariant != PublishVariant.None)
            .Include(lc => lc.Star)
            .Include(lc => lc.User)
            .Include(lc => lc.Device)
            .Include(lc => lc.Observatory)
            .ToListAsync();

        return new ServiceResponse<List<LightCurveDTO>>
        {
            Data = MapCurvesToList(data)
        };
    }

    private List<LightCurveDTO> MapCurvesToList(List<LightCurve> curves)
    {
        List<LightCurveDTO> response = new();
        foreach (var curve in curves) response.Add(_mapper.Map<LightCurveDTO>(curve));

        return response;
    }

    public async Task<ServiceResponse<LightCurveDTO>> LightCurveSingleGet(int curveId)
    {
        var data = await _context.LightCurves
            .Where(lc => lc.Id == curveId)
            .Include(lc => lc.Star)
            .ThenInclude(s => s.StarCatalogs)
            .Include(lc => lc.User)
            .Include(lc => lc.Images)
            .Include(lc => lc.Device)
            .Include(lc => lc.Observatory)
            .FirstOrDefaultAsync();

        if (data is null)
            return ResponseHelper.FailResponse<LightCurveDTO>(Keywords.NotFoundMessage);

        if (!PublishHelper.IsPublic(data.PublishVariant))
            return ResponseHelper.FailResponse<LightCurveDTO>(Keywords.NotPublished);

        var item = _mapper.Map<LightCurveDTO>(data);
        if (PublishHelper.CanShareFile(item.PublishVariant))
            item.DataFileLink = $"{Endpoints.ApiLightCurveBasePath}/{item.Id}/file";
        return new ServiceResponse<LightCurveDTO>
        {
            Data = item
        };
    }

    public async Task<FileContentResult?> LightCurveDataFileGet(int id)
    {
        var lc = await _context.LightCurves.Where(lc => lc.Id == id).Include(lc => lc.Star).FirstOrDefaultAsync();
        if (lc is null)
            return null;
        if (!PublishHelper.CanShareFile(lc.PublishVariant))
            return null;
        var fileData = Encoding.ASCII.GetBytes(lc.DataFileContent);
        var fileResult = new FileContentResult(fileData, "text/plain");
        fileResult.FileDownloadName = $"{lc.Star.Name}_{lc.DateCreated}.txt";
        return fileResult;
    }

    public async Task<ServiceResponse<int>> LightCurvePost(LightCurveAdd lightCurveAdd)
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<int>(Keywords.InvalidToken);

        var lightCurve = _mapper.Map<LightCurve>(lightCurveAdd);
        lightCurve.UserId = userId;

        // Mud Blazor crashes when select has [int?] value type, init to 0
        if (lightCurve.DeviceId == 0)
            lightCurve.DeviceId = null;

        var savedCurve = _context.LightCurves.Add(lightCurve);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<int>(Keywords.PostFailed);

        return new ServiceResponse<int>
        {
            Data = savedCurve.Entity.Id,
            Message = Keywords.PostSucceeded
        };
    }

    public async Task<ServiceResponse<string>> LightCurveSingleValuesGet(int curveId)
    {
        var curve = await _context.LightCurves.Where(c => c.Id == curveId).FirstOrDefaultAsync();
        if (curve is null)
            return ResponseHelper.FailResponse<string>(Keywords.NotFoundMessage);

        if (!PublishHelper.CanShareCurve(curve.PublishVariant))
            return ResponseHelper.FailResponse<string>(Keywords.NotPublished);

        char[] delimiters = { '\r', '\n' };
        List<List<decimal>> valueHolder = new();

        // Load the file content, split by lines
        //     and extract column names (on first line)
        var lines = curve.DataFileContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var columnNames = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        columnNames.RemoveAt(0); // remove # symbol

        // Create a new list for each column
        for (var i = 0; i < columnNames.Count; i++) valueHolder.Add(new List<decimal>());
        // var formatVerified = false;
        foreach (var line in lines)
        {
            // If the line does not begin with a digit, skip
            if (!Regex.Match(line, @"^\d+").Success) continue;
            var lineValues = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // For each column in the data, add to corresponding list
            for (var i = 0; i < lineValues.Length; i++)
            {
                // This cycle is INCREDIBLY cumbersome, but for some reason 
                // decimal.Parse(string) works in normal c# without specifying 
                // number of decimal digits, it does not in asp.net
                // or at least not in this file - might be my error, but there is 
                // not enough time for me to investigate - this will have to do for now
                var currentValue = lineValues[i];

                // First we have to determine the number of decimal digits a string has 
                // E.g. regex match will be 123[.456], the number of digits is match - 1
                // becuase it includes the "." symbol, then the number can be parsed
                var decimalDigits = Regex.Match(currentValue, @"\.\d+").Value.Length - 1;
                var decimalValue = decimal.Parse(currentValue, new NumberFormatInfo
                    { NumberDecimalDigits = decimalDigits, NumberGroupSeparator = "." });
                valueHolder[i].Add(decimalValue);
            }
        }

        // Map column names with the values
        Dictionary<string, List<decimal>> responseData = new();
        for (var i = 0; i < columnNames.Count; i++) responseData.Add(columnNames[i], valueHolder[i]);

        return new ServiceResponse<string>
        {
            Data = JsonSerializer.Serialize(responseData)
        };
    }


    public async Task<ServiceResponse<List<ObservationLogDTO>>> ObservationLogListGet()
    {
        var q = from lc in _context.Set<LightCurve>()
            where lc.PublishVariant != PublishVariant.None
            orderby lc.DateCreated
            group lc by lc.UserId
            into g
            where g.Count() > 0
            orderby g.Key
            select new
            {
                User = g.SingleOrDefault().User,
                Contributions = g.Count()
            };
        var result = await q.ToListAsync();

        List<ObservationLogDTO> data = new();
        foreach (var item in result)
            data.Add(new ObservationLogDTO
            {
                Contributions = item.Contributions,
                User = _mapper.Map<UserSimpleDTO>(item.User)
            });

        return new ServiceResponse<List<ObservationLogDTO>>
        {
            Data = data
        };
    }

    public async Task<ServiceResponse<ObservationLogDetailDTO>> ObservationLogSingleGet(string id)
    {
        var data = await _context.LightCurves
            .Where(lc => lc.UserId == id && lc.PublishVariant != PublishVariant.None)
            .Include(lc => lc.Device)
            .Include(lc => lc.Observatory)
            .Include(lc => lc.Star)
            .ToListAsync();

        var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        if (user is null) return ResponseHelper.FailResponse<ObservationLogDetailDTO>(Keywords.NotFoundMessage);

        List<LightCurveDTO> curveDtos = new();

        foreach (var curve in data)
            curveDtos.Add(_mapper.Map<LightCurveDTO>(curve));
        var stars = data.DistinctBy(lc => lc.StarId).Select(lc => lc.Star).ToList();
        List<StarBasicDTO> starDtos = new();
        foreach (var star in stars) starDtos.Add(_mapper.Map<StarBasicDTO>(star));

        return new ServiceResponse<ObservationLogDetailDTO>
        {
            Data = new ObservationLogDetailDTO
            {
                Curves = curveDtos,
                User = _mapper.Map<UserDTO>(user),
                DistinctStars = starDtos
            }
        };
    }
}