using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.LightCurveService;

public class LightCurveService : ILightCurveService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public LightCurveService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<List<LightCurveDTO>>> GetLightCurvesAsync()
    {
        var data = await _context.LightCurves
            .Include(lc => lc.Star)
            .ToListAsync();
        var response = new List<LightCurveDTO>();
        foreach (var curve in data)
            response.Add(new LightCurveDTO
            {
                Star = new StarDTO
                {
                    Name = curve.Star.Name,
                    Id = curve.Star.Id
                },
                Id = curve.Id
            });

        return new ServiceResponse<List<LightCurveDTO>>
        {
            Data = response
        };
    }

    public async Task<ServiceResponse<LightCurveDTO>> GetLightCurveAsync(int curveId)
    {
        var data = await _context.LightCurves
            .Where(lc => lc.Id == curveId)
            .Include(lc => lc.Star)
            .ThenInclude(s => s.StarCatalogs)
            .Include(lc => lc.User)
            .Include(lc => lc.Device)
            .Include(lc => lc.Observatory)
            .FirstOrDefaultAsync();

        if (data is null)
            return new ServiceResponse<LightCurveDTO>
            {
                Success = false
            };


        var resData = new LightCurveDTO
        {
            Id = data.Id,
            Star = new StarDTO
            {
                Name = data.Star.Name,
                Id = data.Star.Id,
                StarCatalogs = data.Star.StarCatalogs
            },
            DateCreated = data.DateCreated,
            User = new UserDTO
            {
                Id = data.User.Id,
                Name = data.User.Email
            }
        };

        var linePattern = @"\#\sVAR\s(.*)";
        var regRes = Regex.Matches(data.DataFileContent, linePattern);
        var metaLine = regRes.Count > 0 ? regRes[0].Groups[1].Value : null;
        if (metaLine is { Length: > 0 })
        {
            var metaPattern = @"\s*(\S+):\s(\S+)";
            var metaMatches = Regex.Matches(metaLine, metaPattern);
            Dictionary<string, string> meta = new();
            foreach (Match match in metaMatches) meta.Add(match.Groups[1].Value.ToUpper(), match.Groups[2].Value);
            resData.Meta = meta;
        }


        if (data.Device is not null)
            resData.Device = new DeviceDTO
            {
                Id = data.Device.Id,
                Name = data.Device.Name
            };

        if (data.Observatory is not null)
            resData.Observatory = new ObservatoryDTO
            {
                Address = data.Observatory.Address,
                Longitude = data.Observatory.Longitude,
                Latitude = data.Observatory.Latitude,
                Id = data.Observatory.Id
            };
        return new ServiceResponse<LightCurveDTO>
        {
            Data = resData
        };
    }

    public async Task<ServiceResponse<int>> AddLightCurveAsync(LightCurveAdd lightCurveAdd)
    {
        var userId = _authService.GetUserId();
        var lightCurve = new LightCurve
        {
            UserId = userId,
            PublishVariant = lightCurveAdd.PublishVariant,
            StarId = lightCurveAdd.StarId,
            DataFileContent = lightCurveAdd.DataFileContent,
            ImageFileName = "TODO",
            DeviceId = lightCurveAdd.DeviceId
        };

        var savedCurve = _context.LightCurves.Add(lightCurve);
        await _context.SaveChangesAsync();

        return new ServiceResponse<int>
        {
            Data = savedCurve.Entity.Id
        };
    }

    public async Task<ServiceResponse<string>> GetValuesFromCurveAsync(int curveId)
    {
        var curve = await _context.LightCurves.Where(c => c.Id == curveId).FirstOrDefaultAsync();
        if (curve is null)
            return new ServiceResponse<string>
            {
                Data = "Not Found",
                Success = false
            };

        char[] delimiters = { '\r', '\n' };

        // TODO can be different amount of decimal digits
        var format = new NumberFormatInfo { NumberDecimalDigits = 6, NumberGroupSeparator = "." };
        List<List<decimal>> valueHolder = new();

        // Load the file content, split by lines
        //     and extract column names (on first line)
        var lines = curve.DataFileContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var columnNames = lines[0].Split(" ").ToList();
        columnNames.RemoveAt(0); // remove # symbol

        // Create a new list for each column
        for (var i = 0; i < columnNames.Count; i++) valueHolder.Add(new List<decimal>());
        foreach (var line in lines)
        {
            if (line.StartsWith("#")) continue;
            var lineValues = line.Split(" ");
            // For each column in the data, add to corresponding list
            for (var i = 0; i < lineValues.Length; i++) valueHolder[i].Add(decimal.Parse(lineValues[i], format));
        }

        // Map column names with the values
        Dictionary<string, List<decimal>> responseData = new();
        for (var i = 0; i < columnNames.Count; i++) responseData.Add(columnNames[i], valueHolder[i]);

        return new ServiceResponse<string>
        {
            Data = JsonSerializer.Serialize(responseData)
        };
    }
}