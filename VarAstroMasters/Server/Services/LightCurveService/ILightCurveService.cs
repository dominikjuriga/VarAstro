namespace VarAstroMasters.Server.Services.LightCurveService;

public interface ILightCurveService
{
    Task<ServiceResponse<List<LightCurveDTO>>> LightCurveListGet();
    Task<ServiceResponse<LightCurveDTO>> LightCurveSingleGet(int curveId);
    Task<ServiceResponse<int>> LightCurvePost(LightCurveAdd lightCurveAdd);
    Task<ServiceResponse<string>> LightCurveSingleValuesGet(int curveId);
}