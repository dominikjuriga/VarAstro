using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.StarService;

public class StarService : IStarService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public StarService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<List<StarDTO>>> GetStarsAsync()
    {
        var data = await _context.Stars.ToListAsync();
        var stars = new List<StarDTO>();
        foreach (var star in data)
            stars.Add(new StarDTO
            {
                Name = star.Name,
                Id = star.Id
            });
        return new ServiceResponse<List<StarDTO>>
        {
            Data = stars
        };
    }

    public async Task<ServiceResponse<StarDTO>> GetStarAsync(int starId)
    {
        var star = await _context.Stars
            .Include(s => s.LightCurves)
            .ThenInclude(lc => lc.User)
            .Include(s => s.StarCatalogs)
            .Include(s => s.StarPublish)
            .Include(s => s.StarVariability)
            .FirstOrDefaultAsync(s => s.Id == starId);
        if (star is not null)
        {
            var curves = new List<LightCurveDTO>();
            foreach (var curve in star.LightCurves)
                curves.Add(new LightCurveDTO
                {
                    Id = curve.Id,
                    User = new UserDTO
                    {
                        Id = curve.User.Id,
                        Name = curve.User.UserName
                    }
                });
            return new ServiceResponse<StarDTO>
            {
                Data = new StarDTO
                {
                    Id = star.Id,
                    LightCurves = curves,
                    Name = star.Name,
                    StarCatalogs = star.StarCatalogs,
                    StarVariability = star.StarVariability
                }
            };
        }

        return new ServiceResponse<StarDTO>
        {
            Success = false
        };
    }

    public async Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery)
    {
        var data = await _context.Stars
            .Where(s => s.Name.ToLower().Contains(searchQuery.ToLower()))
            .ToListAsync();
        List<StarDTO> dtos = new();
        foreach (var star in data)
            dtos.Add(new StarDTO
            {
                Id = star.Id,
                Name = star.Name
            });
        var response = new ServiceResponse<StarSearchDTO>
        {
            Data = new StarSearchDTO
            {
                Data = dtos
            }
        };

        return response;
    }

    public async Task<ServiceResponse<int>> CreateDraft(StarDraftAdd starDraftAdd)
    {
        var userId = _authService.GetUserId();
        var starDraft = new StarDraft
        {
            Name = starDraftAdd.Name,
            Ra = starDraftAdd.Ra,
            Dec = starDraftAdd.Dec,
            UserId = userId
        };
        _context.StarsDrafts.Add(starDraft);

        await _context.SaveChangesAsync();
        return new ServiceResponse<int>
        {
            Data = starDraft.Id,
            Message = "Používateľská hviezda vytvorená"
        };
    }

    public async Task<ServiceResponse<StarDraft>> GetDraft(int id)
    {
        var sd = await _context.StarsDrafts
            .Where(sd => sd.Id == id)
            .FirstOrDefaultAsync();

        return new ServiceResponse<StarDraft>
        {
            Data = sd
        };
    }

    public async Task<ServiceResponse<List<StarDraft>>> GetDraftList()
    {
        var userId = _authService.GetUserId();
        var sd = await _context.StarsDrafts
            .Where(sd => sd.UserId == userId)
            .ToListAsync();

        return new ServiceResponse<List<StarDraft>>
        {
            Data = sd
        };
    }

    public async Task<ServiceResponse<List<ObservationLogDTO>>> GetObservationLogList()
    {
        var q = from lc in _context.Set<LightCurve>()
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

    public async Task<ServiceResponse<ObservationLogDetailDTO>> GetObservationLog(string id)
    {
        var data = await _context.LightCurves
            .Where(lc => lc.UserId == id)
            .Include(lc => lc.Device)
            .Include(lc => lc.Observatory)
            .Include(lc => lc.Star)
            .ToListAsync();
        var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        List<LightCurveDTO> response = new();
        foreach (var lc in data)
        {
            var dto = new LightCurveDTO
            {
                Id = lc.Id,
                DateCreated = lc.DateCreated,
                Star = new StarDTO
                {
                    Id = lc.Star.Id,
                    Name = lc.Star.Name
                }
            };
            if (lc.Device is not null)
                dto.Device = new DeviceDTO
                {
                    Name = lc.Device.Name
                };
            if (lc.Observatory is not null)
                dto.Observatory = new ObservatoryDTO
                {
                    Address = lc.Observatory.Address
                };
            response.Add(dto);
        }

        return new ServiceResponse<ObservationLogDetailDTO>
        {
            Data = new ObservationLogDetailDTO
            {
                Curves = response,
                User = new UserDTO
                {
                    Name = user.UserName
                }
            }
        };
    }
}