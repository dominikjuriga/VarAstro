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
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<LightCurveDTO>>>(Endpoints.ApiLightCurveGetAll);
        if (response is {Data: not null})
        {
            return response.Data;
        }

        return new List<LightCurveDTO>();
    }

    public async Task<LightCurveDTO> GetLightCurve(int LcId)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<LightCurveDTO>>($"{Endpoints.ApiLightCurveGetSingle}/{LcId}");
        if (response is {Data: not null})
        {
            return response.Data;
        }

        return new LightCurveDTO();
    }
}