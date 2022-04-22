namespace VarAstroMasters.Server.Services.LightCurveService;

public interface ILightCurveService
{
    Task<ServiceResponse<List<LightCurveDTO>>> GetLightCurvesAsync();
    Task<ServiceResponse<LightCurveDTO>> GetLightCurveAsync(int curveId);
    Task<ServiceResponse<LightCurveDTO>> AddLightCurveAsync(LightCurveAdd lightCurveAdd);
}