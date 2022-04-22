namespace VarAstroMasters.Client.Services.LightCurveService;

public interface ILightCurveService
{
    Task<List<LightCurveDTO>> GetLightCurves();
    Task<LightCurveDTO> GetLightCurve(int LcId);
}