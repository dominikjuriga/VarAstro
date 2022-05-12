using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VarAstroMasters.Shared.CompositeKeys;

namespace VarAstroMasters.Server.Services.StarService;

public class StarService : IStarService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StarService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<Star>>> StarListGet()
    {
        var data = await _context.Stars.ToListAsync();

        return new ServiceResponse<List<Star>>
        {
            Data = data
        };
    }

    public async Task<ServiceResponse<StarDTO>> StarSingleGet(int starId)
    {
        var star = await _context.Stars
            .Include(s => s.LightCurves)
            .ThenInclude(lc => lc.User)
            .Include(s => s.StarCatalogs)
            .Include(s => s.StarPublish)
            .Include(s => s.StarVariability)
            .FirstOrDefaultAsync(s => s.Id == starId);

        if (star is null) return ResponseHelper.FailResponse<StarDTO>(Keywords.NotFoundMessage);

        List<LightCurveDTO> curveDtos = new();
        foreach (var curve in star.LightCurves)
            curveDtos.Add(_mapper.Map<LightCurveDTO>(curve));
        var item = _mapper.Map<StarDTO>(star);
        item.LightCurves = curveDtos;

        return new ServiceResponse<StarDTO>
        {
            Data = item
        };
    }

    public async Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery)
    {
        var stars = await _context.Stars
            .Where(s =>
                s.Name.ToLower().Contains(searchQuery.ToLower()) ||
                s.StarCatalogs.Any(sc => sc.CrossId.ToLower() == searchQuery.ToLower()))
            .ToListAsync();

        var msg = stars.Count == 0
            ? Keywords.SearchFailed
            : $"{Keywords.SearchSucceeded} {stars.Count}.";
        List<StarDTO> dtos = new();
        foreach (var star in stars) dtos.Add(_mapper.Map<StarDTO>(star));

        return new ServiceResponse<StarSearchDTO>
        {
            Data = new StarSearchDTO
            {
                Data = dtos
            },
            Message = msg
        };
    }

    public async Task<ServiceResponse<StarSearchDTO>> SearchByCoords(string searchQuery)
    {
        StarCoordDTO? location;
        try
        {
            location = JsonSerializer.Deserialize<StarCoordDTO>(searchQuery);
        }
        catch (Exception e)
        {
            return ResponseHelper.FailResponse<StarSearchDTO>(Keywords.InvalidValues);
        }

        if (location is null) return ResponseHelper.FailResponse<StarSearchDTO>("ASd");
        var raRadians = RaToRadians(location.RaH, location.RaM, location.RaS);
        var decRadians = DecToRadians(location.DecD, location.DecM, location.DecS);

        // Threshold in radians, because of float inaccuracy - Needs to be determined later on
        var threshold = 1;

        if (raRadians is null || decRadians is null)
            return ResponseHelper.FailResponse<StarSearchDTO>(Keywords.ValuesOutOfBounds);

        var stars = await _context.Stars.Where(s =>
                (s.RA > raRadians - threshold && s.RA < raRadians + threshold && s.DEC > decRadians - threshold &&
                 s.DEC < decRadians + threshold) ||
                s.StarCatalogs.Any(sc =>
                    sc.Ra > raRadians - threshold && sc.Ra < raRadians + threshold && sc.Dec > decRadians - threshold &&
                    sc.Dec < decRadians + threshold)
            )
            .ToListAsync();

        var msg = stars.Count == 0
            ? Keywords.SearchFailed
            : $"{Keywords.SearchSucceeded} {stars.Count}. (odchýlka +-{threshold}rad)";

        List<StarDTO> dtos = new();
        foreach (var star in stars) dtos.Add(_mapper.Map<StarDTO>(star));

        return new ServiceResponse<StarSearchDTO>
        {
            Data = new StarSearchDTO
            {
                Data = dtos
            },
            Message = msg
        };
    }

    private double? RaToRadians(int? hours, int? minutes, int? seconds)
    {
        if (hours is null || minutes is null || seconds is null) return null;
        return hours * 15 + minutes / 4d + seconds / 240d;
    }

    private double? DecToRadians(int? degrees, int? minutes, int? seconds)
    {
        if (degrees is null || minutes is null || seconds is null) return null;
        if (degrees > 0)
            return degrees + minutes / 60d + seconds / 3600d;
        else
            return degrees - (double)minutes / 60d - (double)seconds / 3600d;
    }


    public async Task<ServiceResponse<StarPublish>> PublicationSingleGet(int starId)
    {
        var star = await _context.Stars
            .Where(s => s.Id == starId)
            .Include(s => s.StarPublish)
            .FirstOrDefaultAsync();

        if (star is null)
            return ResponseHelper.FailResponse<StarPublish>(Keywords.NotFoundMessage);

        if (star is { StarPublish: null })
            return new ServiceResponse<StarPublish>
            {
                Data = new StarPublish
                {
                    StarName = star.Name
                }
            };

        star.StarPublish.StarName = star.Name;
        return new ServiceResponse<StarPublish>
        {
            Data = star.StarPublish
        };
    }

    public async Task<ServiceResponse<StarPublish>> PublicationPost(StarPublish starPublish)
    {
        var star = await _context.Stars
            .Where(s => s.Id == starPublish.StarId)
            .Include(s => s.StarPublish)
            .FirstOrDefaultAsync();

        if (star is null)
            return ResponseHelper.FailResponse<StarPublish>(Keywords.NotFoundMessage);

        star.StarPublish = starPublish;
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<StarPublish>(Keywords.PostFailed);

        return new ServiceResponse<StarPublish>
        {
            Data = starPublish,
            Message = Keywords.PostSucceeded
        };
    }


    public async Task<ServiceResponse<bool>> StarCatalogSetAsPrimaryPost(StarCatalogCK identification)
    {
        var sc = await _context.StarCatalog
            .Where(sc => sc.StarId == identification.StarId && sc.CatalogId == identification.CatalogId)
            .FirstOrDefaultAsync();

        if (sc is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        var items = _context.StarCatalog
            .Where(sc => sc.StarId == identification.StarId);

        foreach (var item in items) item.Primary = false;
        sc.Primary = true;
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<bool>(Keywords.PostFailed);

        return new ServiceResponse<bool>
        {
            Message = Keywords.PostSucceeded
        };
    }

    public async Task<ServiceResponse<List<StarCatalog>>> StarCatalogListForStarSingleGet(int starId)
    {
        var star = await _context.Stars
            .Where(s => s.Id == starId)
            .Include(s => s.StarCatalogs)
            .FirstOrDefaultAsync();

        if (star is null)
            return ResponseHelper.FailResponse<List<StarCatalog>>(Keywords.NotFoundMessage);

        return new ServiceResponse<List<StarCatalog>>
        {
            Data = star.StarCatalogs
        };
    }

    public async Task<ServiceResponse<StarCatalog>> StarCatalogPut(StarCatalog starCatalog)
    {
        var sc = _context.StarCatalog.Any(sc =>
            sc.StarId == starCatalog.StarId && sc.CatalogId == starCatalog.CatalogId);
        if (!sc)
            return ResponseHelper.FailResponse<StarCatalog>(Keywords.NotFoundMessage);

        _context.StarCatalog.Update(starCatalog);

        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<StarCatalog>(Keywords.PutFailed);

        return new ServiceResponse<StarCatalog>
        {
            Data = starCatalog,
            Message = Keywords.PutSucceeded
        };
    }

    public async Task<ServiceResponse<StarCatalog>> StarCatalogPost(StarCatalog starCatalog)
    {
        if (_context.StarCatalog.Any(sc =>
                sc.StarId == starCatalog.StarId && sc.CatalogId == starCatalog.CatalogId))
            return ResponseHelper.FailResponse<StarCatalog>(Keywords.AlreadyExists);

        if (!_context.StarCatalog.Any(sc =>
                sc.StarId == starCatalog.StarId))
            starCatalog.Primary = true;

        _context.StarCatalog.Add(starCatalog);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<StarCatalog>(Keywords.PostFailed);

        return new ServiceResponse<StarCatalog>
        {
            Data = starCatalog,
            Message = Keywords.PostSucceeded
        };
    }

    public async Task<ServiceResponse<bool>> StarCatalogDelete(int starId, string catalogId)
    {
        var sc = await _context.StarCatalog
            .Where(sc => sc.StarId == starId && sc.CatalogId == catalogId)
            .FirstOrDefaultAsync();

        if (sc is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        if (sc is { Primary: true })
            return ResponseHelper.FailResponse<bool>(Keywords.CannotDeletePrimaryCat);

        _context.StarCatalog.Remove(sc);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<bool>(Keywords.DeleteFailed);

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = Keywords.DeleteSucceeded
        };
    }

    public async Task<ServiceResponse<List<Catalog>>> CatalogListGet()
    {
        var data = await _context.Catalogs.ToListAsync();
        return new ServiceResponse<List<Catalog>>
        {
            Data = data
        };
    }

    public async Task<ServiceResponse<bool>> CatalogDelete(string catalogName)
    {
        var catalog = await _context.Catalogs.Where(c => c.Name == catalogName).FirstOrDefaultAsync();
        if (catalog is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        _context.Catalogs.Remove(catalog);
        var result = await _context.SaveChangesAsync();
        if (result == 0)
            return ResponseHelper.FailResponse<bool>(Keywords.DeleteFailed);

        return new ServiceResponse<bool>
        {
            Success = true,
            Message = Keywords.DeleteSucceeded
        };
    }

    public async Task<ServiceResponse<Catalog>> CatalogPost(Catalog catalog)
    {
        var catalogDb = _context.Catalogs.Any(c => c.Name == catalog.Name);
        if (catalogDb)
            return ResponseHelper.FailResponse<Catalog>(Keywords.AlreadyExists);

        var res = _context.Catalogs.Add(new Catalog
        {
            Name = catalog.Name
        });
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<Catalog>(Keywords.PostFailed);

        return new ServiceResponse<Catalog>
        {
            Data = catalog,
            Message = Keywords.PostSucceeded
        };
    }
}