using VarAstroMasters.Shared.CompositeKeys;

namespace VarAstroMasters.Server.Services.StarService;

public interface IStarService
{
    Task<ServiceResponse<StarDTO>> StarSingleGet(int starId);
    Task<ServiceResponse<List<StarDTO>>> StarListGet();
    Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery);

    Task<ServiceResponse<StarPublish>> PublicationSingleGet(int starId);
    Task<ServiceResponse<bool>> PublicationPost(StarPublish starPublish);
    Task<ServiceResponse<bool>> StarCatalogSetAsPrimaryPost(StarCatalogCK identification);
    Task<ServiceResponse<List<ObservationLogDTO>>> ObservationLogListGet();
    Task<ServiceResponse<ObservationLogDetailDTO>> ObservationLogSingleGet(string id);
    Task<ServiceResponse<List<StarCatalog>>> StarCatalogListForStarSingleGet(int starId);
    Task<ServiceResponse<StarCatalog>> StarCatalogPost(StarCatalog starCatalog);
    Task<ServiceResponse<bool>> StarCatalogDelete(int starId, string catalogId);
    Task<ServiceResponse<List<Catalog>>> CatalogListGet();
}