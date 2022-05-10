namespace VarAstroMasters.Client.Services.LightCurveService;

public class LightCurveService : ILightCurveService
{
    private readonly HttpClient _http;

    public LightCurveService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<List<LightCurveDTO>>> LightCurveListGet()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<LightCurveDTO>>>(Endpoints.ApiLightCurveListGet);
        return response;
    }

    public async Task<ServiceResponse<LightCurveDTO>> LightCurveSingleGet(int lightCurveId)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<LightCurveDTO>>(
                $"{Endpoints.ApiLightCurveSingleGet}/{lightCurveId}");
        return response;
    }

    public async Task<ServiceResponse<string>> LightCurveSingleValuesGet(int lightCurveId)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<string>>(
                $"{Endpoints.ApiLightCurveSingleValuesGet}/{lightCurveId}/values");
        return response;
    }

    public async Task<ServiceResponse<int>> LightCurvePost(LightCurveAdd lightCurveAdd)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiLightCurvePost, lightCurveAdd);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }


    public async Task<ServiceResponse<List<ObservationLogDTO>>> GetObservationLogList()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<ObservationLogDTO>>>(Endpoints
                .ApiStarObservationLogListGet);
        return response;
    }

    public async Task<ServiceResponse<ObservationLogDetailDTO>> GetObservationLog(string id)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<ObservationLogDetailDTO>>(
                $"{Endpoints.ApiStarObservationLogSingleGet}/{id}");
        return response;
    }
}