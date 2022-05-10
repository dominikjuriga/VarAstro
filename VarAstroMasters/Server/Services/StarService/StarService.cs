using Microsoft.EntityFrameworkCore;
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

    public async Task<ServiceResponse<List<Star>>> Search(string searchQuery)
    {
        var data = await _context.Stars
            .Where(s => s.Name.ToLower().Contains(searchQuery.ToLower()))
            .ToListAsync();

        var msg = data.Count == 0 ? Keywords.SearchFailed : $"{Keywords.SearchSucceeded} {data.Count} záznamov.";

        return new ServiceResponse<List<Star>>
        {
            Data = data,
            Message = msg
        };
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
        var sc = await _context.StarCatalog.Where(sc =>
            sc.StarId == starCatalog.StarId && sc.CatalogId == starCatalog.CatalogId).FirstOrDefaultAsync();
        if (sc is null)
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