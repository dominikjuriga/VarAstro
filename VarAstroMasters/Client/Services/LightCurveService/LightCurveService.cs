namespace VarAstroMasters.Client.Services.LightCurveService;

public class LightCurveService : ILightCurveService
{
    private readonly HttpClient _http;

    public LightCurveService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<LightCurveDTO>> GetLightCurves()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<LightCurveDTO>>>(Endpoints.ApiLightCurveGetAll);
        if (response is { Data: not null }) return response.Data;

        return new List<LightCurveDTO>();
    }

    public async Task<LightCurveDTO> GetLightCurve(int lightCurveId)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<LightCurveDTO>>(
                $"{Endpoints.ApiLightCurveGetSingle}/{lightCurveId}");
        if (response is { Data: not null }) return response.Data;

        return new LightCurveDTO();
    }

    public async Task<string> GetLightCurveValues(int lightCurveId)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<string>>(
                $"{Endpoints.ApiLightCurveGetValues}/{lightCurveId}/values");
        if (response is { Data: not null }) return response.Data;

        return "chyba";
    }

    public async Task<bool> AddLightCurve(LightCurveAdd lightCurveAdd)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiLightCurveAdd, lightCurveAdd);
        return response != null;
    }
}