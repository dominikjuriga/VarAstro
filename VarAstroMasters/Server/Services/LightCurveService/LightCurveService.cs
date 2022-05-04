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
        var lightCurve = new LightCurve
        {
            StarId = lightCurveAdd.StarId,
            UserId = _authService.GetUserId()
        };
        _context.LightCurves.Add(lightCurve);
        await _context.SaveChangesAsync();

        return new ServiceResponse<LightCurveDTO>
        {
            Data = new LightCurveDTO
            {
                Id = lightCurve.Id
            }
        };
    }
}