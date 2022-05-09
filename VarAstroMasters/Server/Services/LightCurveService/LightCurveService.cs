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

    public LightCurveService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
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
        if (data.Count == 0)
            return new ServiceResponse<List<LightCurveDTO>>
            {
                Data = new List<LightCurveDTO>()
            };
        var response = MapCurversToList(data);

        return new ServiceResponse<List<LightCurveDTO>>
        {
            Data = response
        };
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
            return new ServiceResponse<LightCurveDTO>
            {
                Success = false,
                Message = Keywords.NotFoundMessage
            };

        if (!IsPublic(data.PublishVariant))
            return new ServiceResponse<LightCurveDTO>
            {
                Success = false,
                Message = Keywords.NotPublished
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
            },
            Comment = data.Comment
        };

        if (CanShareMap(data.PublishVariant))
            resData.Images = data.Images;

        // Include file content when
        if (CanShareFile(data.PublishVariant))
            resData.DataFileLink = $"{Endpoints.ApiLightCurveBasePath}/{data.Id}/file";

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

    public async Task<FileContentResult?> LightCurveDataFileGet(int id)
    {
        var lc = await _context.LightCurves.Where(lc => lc.Id == id).Include(lc => lc.Star).FirstOrDefaultAsync();
        if (lc is null)
            return null;
        if (!CanShareFile(lc.PublishVariant))
            return null;
        var fileData = Encoding.ASCII.GetBytes(lc.DataFileContent);
        var fileResult = new FileContentResult(fileData, "text/plain");
        fileResult.FileDownloadName = $"{lc.Star.Name}_{lc.DateCreated}.txt";
        return fileResult;
    }

    public async Task<ServiceResponse<int>> LightCurvePost(LightCurveAdd lightCurveAdd)
    {
        var userId = _authService.GetUserId();
        var lightCurve = new LightCurve
        {
            UserId = userId,
            PublishVariant = lightCurveAdd.PublishVariant,
            StarId = lightCurveAdd.StarId,
            DataFileContent = lightCurveAdd.DataFileContent,
            DeviceId = lightCurveAdd.DeviceId,
            Images = lightCurveAdd.Images
        };

        var savedCurve = _context.LightCurves.Add(lightCurve);
        await _context.SaveChangesAsync();

        return new ServiceResponse<int>
        {
            Data = savedCurve.Entity.Id
        };
    }

    public async Task<ServiceResponse<string>> LightCurveSingleValuesGet(int curveId)
    {
        var curve = await _context.LightCurves.Where(c => c.Id == curveId).FirstOrDefaultAsync();
        if (curve is null)
            return new ServiceResponse<string>
            {
                Data = Keywords.NotFoundMessage,
                Success = false
            };

        if (!CanShareCurve(curve.PublishVariant))
            return new ServiceResponse<string>
            {
                Message = "Táto krivka nie je zverejnená."
            };

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
                User = new UserSimpleDTO
                {
                    Id = item.User.Id,
                    Name = item.User.UserName
                }
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

        var response = MapCurversToList(data);

        return new ServiceResponse<ObservationLogDetailDTO>
        {
            Data = new ObservationLogDetailDTO
            {
                Curves = response,
                User = new UserDTO
                {
                    Name = $"{user.FirstName} {user.LastName}"
                }
            }
        };
    }

    private List<LightCurveDTO> MapCurversToList(List<LightCurve> curves)
    {
        List<LightCurveDTO> response = new();
        foreach (var curve in curves)
        {
            var currentDTO = new LightCurveDTO
            {
                Star = new StarDTO
                {
                    Name = curve.Star.Name,
                    Id = curve.Star.Id
                },
                User = new UserDTO
                {
                    Id = curve.User.Id,
                    Name = $"{curve.User.FirstName} {curve.User.LastName}"
                },
                Id = curve.Id,
                DateCreated = curve.DateCreated
            };
            if (curve.Device is not null)
                currentDTO.Device = new DeviceDTO
                {
                    Name = curve.Device.Name
                };
            if (curve.Observatory is not null)
                currentDTO.Observatory = new ObservatoryDTO
                {
                    Address = curve.Observatory.Address
                };
            response.Add(currentDTO);
        }

        return response;
    }

    private List<PublishVariant> MapPermissions = new()
        { PublishVariant.All, PublishVariant.OnlyMap, PublishVariant.OnlyMapAndCurve };

    private List<PublishVariant> CurvePermissions = new()
        { PublishVariant.All, PublishVariant.OnlyMapAndCurve };

    private List<PublishVariant> FilePermissions = new()
        { PublishVariant.All };

    private bool IsPublic(PublishVariant variant)
    {
        return variant != PublishVariant.None;
    }

    private bool CanShareMap(PublishVariant variant)
    {
        return MapPermissions.Contains(variant);
    }

    private bool CanShareCurve(PublishVariant variant)
    {
        return CurvePermissions.Contains(variant);
    }

    private bool CanShareFile(PublishVariant variant)
    {
        return FilePermissions.Contains(variant);
    }
}