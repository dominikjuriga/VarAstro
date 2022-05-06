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
}