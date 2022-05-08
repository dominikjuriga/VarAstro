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
            await _http.GetFromJsonAsync<ServiceResponse<List<LightCurveDTO>>>(Endpoints.ApiLightCurveListGet);
        if (response is { Data: not null }) return response.Data;

        return new List<LightCurveDTO>();
    }

    public async Task<LightCurveDTO> GetLightCurve(int lightCurveId)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<LightCurveDTO>>(
                $"{Endpoints.ApiLightCurveSingleGet}/{lightCurveId}");
        if (response is { Data: not null }) return response.Data;

        return new LightCurveDTO();
    }

    public async Task<ServiceResponse<string>> GetLightCurveValues(int lightCurveId)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<string>>(
                $"{Endpoints.ApiLightCurveSingleValuesGet}/{lightCurveId}/values");
        return response;
    }

    public async Task<ServiceResponse<int>> AddLightCurve(LightCurveAdd lightCurveAdd)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiLightCurvePost, lightCurveAdd);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }
}