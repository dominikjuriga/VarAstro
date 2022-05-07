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

    public async Task<ServiceResponse<List<StarDraft>>> GetDraftList()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<StarDraft>>>(Endpoints.ApiStarGetDraftList);
        return response;
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