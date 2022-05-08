namespace VarAstroMasters.Client.Services.LightCurveService;

public interface ILightCurveService
{
    Task<List<LightCurveDTO>> LightCurveListGet();
    Task<LightCurveDTO> LightCurveSingleGet(int lightCurveId);
    Task<ServiceResponse<string>> LightCurveSingleValuesGet(int lightCurveId);
    Task<ServiceResponse<int>> LightCurvePost(LightCurveAdd lightCurveAdd);
}