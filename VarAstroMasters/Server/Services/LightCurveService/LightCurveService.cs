using System.Globalization;
using System.Text.Json;
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
            .Include(lc => lc.User)
            .FirstOrDefaultAsync();
        if (data is null)
            return new ServiceResponse<LightCurveDTO>
            {
                Success = false
            };

        return new ServiceResponse<LightCurveDTO>
        {
            Data = new LightCurveDTO
            {
                Id = data.Id,
                Star = new StarDTO
                {
                    Name = data.Star.Name,
                    Id = data.Star.Id
                },
                User = new UserDTO
                {
                    Id = data.User.Id,
                    Name = data.User.Email
                }
            }
        };
    }

    public async Task<ServiceResponse<LightCurveDTO>> AddLightCurveAsync(LightCurveAdd lightCurveAdd)
    {
        var userId = _authService.GetUserId();
        // var lightCurve = new LightCurve
        // {
        //     StarId = lightCurveAdd.StarId,
        //     UserId = _authService.GetUserId()
        // };
        // _context.LightCurves.Add(lightCurve);
        // await _context.SaveChangesAsync();
        //
        // return new ServiceResponse<LightCurveDTO>
        // {
        //     Data = new LightCurveDTO
        //     {
        //         Id = lightCurve.Id
        //     }
        // };
        var lightCurve = new LightCurve
        {
            UserId = userId,
            PublishVariant = lightCurveAdd.PublishVariant,
            StarId = lightCurveAdd.StarId,
            DataFileContent = lightCurveAdd.DataFileContent,
            ImageFileName = "TODO"
        };

        var savedCurve = _context.LightCurves.Add(lightCurve);
        await _context.SaveChangesAsync();

        return new ServiceResponse<LightCurveDTO>
        {
            Data = new LightCurveDTO
            {
                Id = savedCurve.Entity.Id
            }
        };
    }

    public async Task<ServiceResponse<string>> GetValuesFromCurveAsync(int curveId)
    {
        var curve = await _context.LightCurves.Where(c => c.Id == curveId).FirstOrDefaultAsync();
        if (curve is null)
            return new ServiceResponse<string>
            {
                Data = "bad"
            };

        List<List<decimal>> values = new();
        var fileContent = curve.DataFileContent;
        char[] delimiters = { '\r', '\n' };
        var lines = fileContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            if (line.StartsWith("#")) continue;

            var lineValues = line.Split(" ");
            var format = new NumberFormatInfo { NumberDecimalDigits = 6, NumberGroupSeparator = "." };
            var val1 = decimal.Parse(lineValues[0], format);
            var val2 = decimal.Parse(lineValues[1], format);
            values.Add(new List<decimal>
            {
                val1, val2
            });
        }

        return new ServiceResponse<string>
        {
            Data = JsonSerializer.Serialize(values)
        };
    }
}