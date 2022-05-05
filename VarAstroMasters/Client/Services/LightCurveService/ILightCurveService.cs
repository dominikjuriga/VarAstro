namespace VarAstroMasters.Client.Services.LightCurveService;

public interface ILightCurveService
{
    Task<List<LightCurveDTO>> GetLightCurves();
    Task<LightCurveDTO> GetLightCurve(int lightCurveId);
    Task<string> GetLightCurveValues(int lightCurveId);
    Task<ServiceResponse<int>> AddLightCurve(LightCurveAdd lightCurveAdd);
}