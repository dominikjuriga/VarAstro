using Microsoft.EntityFrameworkCore;
using VarAstroMasters.Server.Data;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LightCurveController : ControllerBase
{
    private readonly DataContext _context;

    public LightCurveController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<LightCurveDTO>>>> GetLightCurvesAsync()
    {
        var data = await _context.LightCurves
            .Include(lc => lc.Star)
            .ToListAsync();
        var response = new List<LightCurveDTO>();
        foreach (var curve in data)
        {
            response.Add(new LightCurveDTO
            {
                Value = curve.Value,
                Star = new StarDTO
                {
                    Name = curve.Star.Name,
                    Id = curve.Star.Id,
                },
                Id = curve.Id
            });
        }

        var sr = new ServiceResponse<List<LightCurveDTO>>
        {
            Data = response
        };
        return Ok(sr);
    }
    
    [HttpGet("{LcId}")]
    public async Task<ActionResult<ServiceResponse<LightCurveDTO>>> GetLightCurveAsync(int LcId)
    {
        var data = await _context.LightCurves
            .Where(lc => lc.Id == LcId)
            .Include(lc => lc.Star)
            .Include(lc => lc.User)
            .FirstOrDefaultAsync();
        if (data is null)
        {
            return Ok(new ServiceResponse<LightCurveDTO>
            {
                Success = false
            });
        }

        var sr = new ServiceResponse<LightCurveDTO>
        {
            Data = new LightCurveDTO
            {
                Value = data.Value,
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
        return Ok(sr);
    }
}