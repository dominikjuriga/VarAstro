using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using VarAstroMasters.Shared.CompositeKeys;

namespace VarAstroMasters.Server.Services.StarService;

public class StarService : IStarService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;
    private readonly ILightCurveService _lightCurveService;

    public StarService(DataContext context, IAuthService authService, ILightCurveService lightCurveService)
    {
        _context = context;
        _authService = authService;
        _lightCurveService = lightCurveService;
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
                    },
                    DateCreated = curve.DateCreated
                });

            return new ServiceResponse<StarDTO>
            {
                Data = new StarDTO
                {
                    Id = star.Id,
                    LightCurves = curves,
                    Name = star.Name,
                    StarCatalogs = star.StarCatalogs,
                    StarVariability = star.StarVariability,
                    StarPublish = star.StarPublish
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

    public async Task<ServiceResponse<StarPublish>> GetPublication(int starId)
    {
        var star = await _context.Stars
            .Where(s => s.Id == starId)
            .Include(s => s.StarPublish).FirstOrDefaultAsync();
        star.StarPublish.StarName = star.Name;
        return new ServiceResponse<StarPublish>
        {
            Data = star.StarPublish
        };
    }

    public async Task<ServiceResponse<bool>> SavePublication(StarPublish starPublish)
    {
        var star = await _context.Stars
            .Where(s => s.Id == starPublish.StarId)
            .Include(s => s.StarPublish)
            .FirstOrDefaultAsync();
        if (star is null)
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Hviezda nenájdená."
            };

        star.StarPublish = starPublish;
        await _context.SaveChangesAsync();
        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Publikácia uložená."
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

    public async Task<ServiceResponse<bool>> SetStarCatalogPrimary(StarCatalogCK identification)
    {
        var sc = await _context.StarCatalog
            .Where(sc => sc.StarId == identification.StarId && sc.CatalogId == identification.CatalogId)
            .FirstOrDefaultAsync();

        if (sc is null)
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Záznam nenájdený."
            };

        var items = _context.StarCatalog
            .Where(sc => sc.StarId == identification.StarId);

        foreach (var item in items) item.Primary = false;

        sc.Primary = true;

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Message = "Zmena vykonaná."
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

    public async Task<ServiceResponse<List<StarCatalog>>> GetStarCatalogs(int starId)
    {
        var star = await _context.Stars
            .Where(s => s.Id == starId)
            .Include(s => s.StarCatalogs)
            .FirstOrDefaultAsync();

        if (star is null)
            return new ServiceResponse<List<StarCatalog>>
            {
                Success = false,
                Message = "Hviezda nenájdená"
            };

        return new ServiceResponse<List<StarCatalog>>
        {
            Data = star.StarCatalogs
        };
    }

    public async Task<ServiceResponse<StarCatalog>> SaveStarCatalog(StarCatalog starCatalog)
    {
        if (starCatalog.New)
        {
            if (_context.StarCatalog.Any(sc =>
                    sc.StarId == starCatalog.StarId && sc.CatalogId == starCatalog.CatalogId))
                return new ServiceResponse<StarCatalog>
                {
                    Success = false,
                    Message = "Záznam pre tento katalóg už existuje"
                };
            _context.StarCatalog.Add(starCatalog);
        }
        else
        {
            var sc = await _context.StarCatalog.Where(sc =>
                sc.StarId == starCatalog.StarId && sc.CatalogId == starCatalog.CatalogId).FirstOrDefaultAsync();
            if (sc is null)
                return new ServiceResponse<StarCatalog>
                {
                    Success = false,
                    Message = "Záznam nenájdený"
                };

            sc.Ra = starCatalog.Ra;
            sc.Dec = starCatalog.Dec;
            sc.CatalogId = starCatalog.CatalogId;
            sc.Mag = starCatalog.Mag;
            sc.CrossId = starCatalog.CrossId;

            await _context.SaveChangesAsync();
            return new ServiceResponse<StarCatalog>
            {
                Data = sc,
                Message = "Záznam upravený."
            };
        }

        await _context.SaveChangesAsync();
        return new ServiceResponse<StarCatalog>
        {
            Data = starCatalog,
            Message = "Záznam vytvorený."
        };
    }

    public async Task<ServiceResponse<bool>> DeleteStarCatalog(int starId, string catalogId)
    {
        var sc = await _context.StarCatalog
            .Where(sc => sc.StarId == starId && sc.CatalogId == catalogId)
            .FirstOrDefaultAsync();
        if (sc is null)
            return new ServiceResponse<bool>
            {
                Data = false,
                Message = "Záznam neexistuje."
            };
        if (sc is { Primary: true })
            return new ServiceResponse<bool>
            {
                Data = false,
                Message = "Primárny katalóg nie je možné zmazať."
            };

        _context.StarCatalog.Remove(sc);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Záznam zmazaný."
        };
    }

    public async Task<ServiceResponse<List<Catalog>>> GetCatalogs()
    {
        var data = await _context.Catalogs.ToListAsync();
        return new ServiceResponse<List<Catalog>>
        {
            Data = data
        };
    }
}