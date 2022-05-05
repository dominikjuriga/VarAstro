using Microsoft.EntityFrameworkCore;
using VarAstroMasters.Server.Data;
using VarAstroMasters.Server.Services.LightCurveService;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LightCurveController : ControllerBase
{
    private readonly ILightCurveService _lightCurveService;

    public LightCurveController(ILightCurveService lightCurveService)
    {
        _lightCurveService = lightCurveService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<LightCurveDTO>>>> GetLightCurvesAsync()
    {
        var sr = await _lightCurveService.GetLightCurvesAsync();
        return Ok(sr);
    }

    [HttpGet("{lightCurveId}")]
    public async Task<ActionResult<ServiceResponse<LightCurveDTO>>> GetLightCurveAsync(int lightCurveId)
    {
        var sr = await _lightCurveService.GetLightCurveAsync(lightCurveId);
        return Ok(sr);
    }

    [HttpGet("{lightCurveId}/values")]
    public async Task<ActionResult<ServiceResponse<string>>> GetLightCurveValuesAsync(int lightCurveId)
    {
        var sr = await _lightCurveService.GetValuesFromCurveAsync(lightCurveId);
        return Ok(sr);
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<LightCurveDTO>>> AddLightCurveAsync(LightCurveAdd lightCurveAdd)
    {
        var sr = await _lightCurveService.AddLightCurveAsync(lightCurveAdd);
        return Ok(sr);
    }
}