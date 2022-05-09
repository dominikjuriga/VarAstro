using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public class ObservatoryService : IObservatoryService
{
    private readonly HttpClient _http;

    public ObservatoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<List<Observatory>>> ObservatoryListFromTokenGet()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<Observatory>>>(
                Endpoints.ApiObservatoryListGet);
        return response;
    }

    public async Task<ServiceResponse<Observatory>> ObservatoryPost(Observatory observatory)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiObservatoryPost, observatory);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<Observatory>>();
    }

    public async Task<ServiceResponse<bool>> ObservatoryDelete(int modelId)
    {
        var response = await _http.DeleteAsync($"{Endpoints.ApiObservatoryDelete}/{modelId}");
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<Observatory>> ObservatoryPut(Observatory observatory)
    {
        var response = await _http.PutAsJsonAsync(Endpoints.ApiObservatoryPut, observatory);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<Observatory>>();
    }
}