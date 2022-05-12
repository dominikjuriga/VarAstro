namespace VarAstroMasters.Client.Services.StarService;

public class StarService : IStarService
{
    private readonly HttpClient _httpClient;

    public StarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<List<StarDTO>>> GetStarsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<StarDTO>>>(Endpoints.ApiStarAllGet);
        return response;
    }

    public async Task<ServiceResponse<StarDTO>> GetStarAsync(int starId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarDTO>>($"{Endpoints.ApiStarSingleGet}/{starId}");
        return response;
    }

    public async Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarSearchDTO>>(
                $"{Endpoints.ApiStarSearch}/{searchQuery}");
        return response;
    }

    public async Task<ServiceResponse<StarSearchDTO>> SearchByCoords(string searchQuery)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarSearchDTO>>(
                $"{Endpoints.ApiStarSearchByCoords}/{searchQuery}");
        return response;
    }


    public async Task<ServiceResponse<StarPublish>> GetPublication(int starId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarPublish>>(
                $"{Endpoints.ApiStarPublicationGet}/{starId}");
        return response;
    }

    public async Task<ServiceResponse<List<StarCatalog>>> GetStarCatalogs(int starId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<StarCatalog>>>(
                $"{Endpoints.ApiStarSingleCatalogsGet}/{starId}");
        return response;
    }

    public async Task<ServiceResponse<StarCatalog>> StarCatalogPost(StarCatalog starCatalog)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarStarCatalogPost, starCatalog);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<StarCatalog>>();
    }

    public async Task<ServiceResponse<StarCatalog>> StarCatalogPut(StarCatalog starCatalog)
    {
        var response = await _httpClient.PutAsJsonAsync(Endpoints.ApiStarStarCatalogPost, starCatalog);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<StarCatalog>>();
    }

    public async Task<ServiceResponse<StarCatalog>> SaveStarCatalog(StarCatalog starCatalog)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarStarCatalogPost, starCatalog);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<StarCatalog>>();
    }

    public async Task<ServiceResponse<bool>> DeleteStarCatalog(int starId, string catalogId)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoints.ApiStarStarCatalogDelete}/{starId}/{catalogId}");
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<List<Catalog>>> CatalogListGet()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<Catalog>>>(
                $"{Endpoints.ApiCatalogsListGet}");
        return response;
    }

    public async Task<ServiceResponse<bool>> CatalogDelete(string catalogName)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoints.ApiCatalogDelete}/{catalogName}");
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<bool>> SetStarCatalogPrimary(StarCatalogCK identification)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiCatalogPrimaryPost, identification);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<StarPublish>> SavePublication(StarPublish starPublish)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarPublicationPost, starPublish);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<StarPublish>>();
    }


    public async Task<ServiceResponse<Catalog>> CatalogPost(CatalogEdit catalog)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiCatalogPost, catalog);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<Catalog>>();
    }
}