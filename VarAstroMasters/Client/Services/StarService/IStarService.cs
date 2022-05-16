namespace VarAstroMasters.Client.Services.StarService;

public interface IStarService
{
    Task<ServiceResponse<List<StarDTO>>> GetStarsAsync();
    Task<ServiceResponse<StarDTO>> GetStarAsync(int starId);
    Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery);
    Task<ServiceResponse<StarSearchDTO>> SearchByCoords(string searchQuery);
    Task<ServiceResponse<StarPublish>> GetPublication(int starId);
    Task<ServiceResponse<List<StarCatalog>>> GetStarCatalogs(int starId);
    Task<ServiceResponse<StarCatalog>> SaveStarCatalog(StarCatalog starCatalog);
    Task<ServiceResponse<StarCatalog>> StarCatalogPost(StarCatalog starCatalog);
    Task<ServiceResponse<StarCatalog>> StarCatalogPut(StarCatalog starCatalog);
    Task<ServiceResponse<bool>> DeleteStarCatalog(int starId, string catalogId);
    Task<ServiceResponse<List<Catalog>>> CatalogListGet();
    Task<ServiceResponse<bool>> CatalogDelete(string catalogName);
    Task<ServiceResponse<bool>> SetStarCatalogPrimary(StarCatalogCK identification);
    Task<ServiceResponse<StarPublish>> SavePublication(StarPublish starPublish);
    Task<ServiceResponse<StarDTO>> GetStarByCoords(StarCoordDTO coords);

    //////////////////////////
    Task<ServiceResponse<Catalog>> CatalogPost(CatalogEdit catalog);
    Task<ServiceResponse<int>> StarPost(NewStar newStar);
    Task<ServiceResponse<bool>> StarPut(Star star);
    Task<ServiceResponse<Star>> StarSingleWithoutMetaGet(int StarId);
}