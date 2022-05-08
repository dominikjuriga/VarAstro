using VarAstroMasters.Shared.CompositeKeys;

namespace VarAstroMasters.Server.Services.StarService;

public interface IStarService
{
    Task<ServiceResponse<List<StarDTO>>> GetStarsAsync();
    Task<ServiceResponse<StarDTO>> GetStarAsync(int starId);
    Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery);
    Task<ServiceResponse<int>> CreateDraft(StarDraftAdd starDraftAdd);
    Task<ServiceResponse<StarDraft>> GetDraft(int id);
    Task<ServiceResponse<StarPublish>> GetPublication(int starId);
    Task<ServiceResponse<bool>> SavePublication(StarPublish starPublish);
    Task<ServiceResponse<List<StarDraft>>> GetDraftList();
    Task<ServiceResponse<bool>> SetStarCatalogPrimary(StarCatalogCK identification);
    Task<ServiceResponse<List<ObservationLogDTO>>> GetObservationLogList();
    Task<ServiceResponse<ObservationLogDetailDTO>> GetObservationLog(string id);
    Task<ServiceResponse<List<StarCatalog>>> GetStarCatalogs(int starId);
    Task<ServiceResponse<StarCatalog>> SaveStarCatalog(StarCatalog starCatalog);
    Task<ServiceResponse<bool>> DeleteStarCatalog(int starId, string catalogId);
    Task<ServiceResponse<List<Catalog>>> GetCatalogs();
}