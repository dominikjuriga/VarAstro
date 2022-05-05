using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public class ObservatoryService : IObservatoryService
{
    private readonly HttpClient _http;

    public ObservatoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Observatory>> GetMyObservatories()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<Observatory>>>(Endpoints.ApiObservatoryGetObservatories);
        return response.Data;
    }

    public async Task<bool> AddObservatory(Observatory observatoryAdd)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiObservatoryAdd, observatoryAdd);
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}