namespace VarAstroMasters.Client.Services.StarService;

public class StarService : IStarService
{
    private readonly HttpClient _httpClient;

    public StarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StarDTO>> GetStarsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<StarDTO>>>(Endpoints.ApiStarGetAll);
        if (response is { Data: not null }) return response.Data;

        return new List<StarDTO>();
    }

    public async Task<ServiceResponse<StarDTO>> GetStarAsync(int starId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarDTO>>($"{Endpoints.ApiStarGetSingle}/{starId}");
        return response;
    }

    public async Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarSearchDTO>>(
                $"{Endpoints.ApiStarSearch}/{searchQuery}");
        return response;
    }

    public async Task<ServiceResponse<int>> CreateDraft(StarDraftAdd starDraftAdd)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarCreateDraft, starDraftAdd);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<StarDraft>> GetDraft(int id)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarDraft>>($"{Endpoints.ApiStarGetDraft}/{id}");
        return response;
    }

    public async Task<ServiceResponse<StarPublish>> GetPublication(int starId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<StarPublish>>(
                $"{Endpoints.ApiStarGetPublication}/{starId}");
        return response;
    }

    public async Task<ServiceResponse<List<StarDraft>>> GetDraftList()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<StarDraft>>>(Endpoints.ApiStarGetDraftList);
        return response;
    }

    public async Task<ServiceResponse<List<StarCatalog>>> GetStarCatalogs(int starId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<StarCatalog>>>(
                $"{Endpoints.ApiStarGetCatalogs}/{starId}");
        return response;
    }

    public async Task<ServiceResponse<StarCatalog>> SaveStarCatalog(StarCatalog starCatalog)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarPostStarCatalog, starCatalog);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<StarCatalog>>();
    }

    public async Task<ServiceResponse<bool>> DeleteStarCatalog(int starId, string catalogId)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoints.ApiStarDeleteStarCatalog}/{starId}/{catalogId}");
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<List<Catalog>>> GetCatalogs()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<Catalog>>>(
                $"{Endpoints.ApiCatalogsGetCatalogs}");
        return response;
    }

    public async Task<ServiceResponse<bool>> SetStarCatalogPrimary(StarCatalogCK identification)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarSetStarCatalogPrimary, identification);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<bool>> SavePublication(StarPublish starPublish)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoints.ApiStarPostPublication, starPublish);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<List<ObservationLogDTO>>> GetObservationLogList()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<ObservationLogDTO>>>(Endpoints
                .ApiStarGetObservationLogList);
        return response;
    }

    public async Task<ServiceResponse<ObservationLogDetailDTO>> GetObservationLog(string id)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<ObservationLogDetailDTO>>(
                $"{Endpoints.ApiStarGetObservationLog}/{id}");
        return response;
    }
}