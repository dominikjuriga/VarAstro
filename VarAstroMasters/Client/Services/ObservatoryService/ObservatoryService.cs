using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public class ObservatoryService : IObservatoryService
{
    private readonly HttpClient _http;

    public ObservatoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ObservatoryDTO>> GetMyObservatories()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<ObservatoryDTO>>>(
                Endpoints.ApiObservatoryListGet);
        return response.Data;
    }

    public async Task<ServiceResponse<bool>> AddObservatory(ObservatoryAdd observatoryAdd)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiObservatoryPost, observatoryAdd);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<bool>> DeleteObservatory(int modelId)
    {
        var response = await _http.DeleteAsync($"{Endpoints.ApiObservatoryDelete}/{modelId}");
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<ObservatoryDTO>> EditObservatory(ObservatoryEdit observatoryEdit)
    {
        var response = await _http.PutAsJsonAsync(Endpoints.ApiObservatoryEdit, observatoryEdit);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<ObservatoryDTO>>();
    }
}