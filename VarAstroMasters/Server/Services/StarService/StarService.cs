using Microsoft.EntityFrameworkCore;
using VarAstroMasters.Server.Data;

namespace VarAstroMasters.Server.Services.StarService;

public class StarService : IStarService
{
    private readonly DataContext _context;

    public StarService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<List<StarDTO>>> GetStarsAsync()
    {
        var data = await _context.Stars.ToListAsync();
        var stars = new List<StarDTO>();
        foreach (var star in data)
        {
            stars.Add(new StarDTO
            {
                Name = star.Name,
                Id = star.Id
            });
        }
        return new ServiceResponse<List<StarDTO>>
        {
            Data = stars
        };
    }

    public async Task<ServiceResponse<StarDTO>> GetStarAsync(int starId)
    {
        var star = await _context.Stars
            .Include(s => s.LightCurves)
            .FirstOrDefaultAsync(s => s.Id == starId);
        if (star is not null)
        {
            var curves = new List<LightCurveDTO>();
            foreach (var curve in star.LightCurves)
            {
                curves.Add(new LightCurveDTO
                {
                    Id = curve.Id,
                    Value = curve.Value
                });
            }
            return new ServiceResponse<StarDTO>
            {
                Data = new StarDTO
                {
                    Id = star.Id,
                    LightCurves = curves,
                    Name = star.Name
                }
            };
        }

        return new ServiceResponse<StarDTO>
        {
            Success = false
        };
    }
}