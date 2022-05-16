using Microsoft.AspNetCore.Authorization;

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
    public async Task<ActionResult<ServiceResponse<List<LightCurveDTO>>>> LightCurveListGet()
    {
        var sr = await _lightCurveService.LightCurveListGet();
        return Ok(sr);
    }

    [HttpGet("{lightCurveId}")]
    public async Task<ActionResult<ServiceResponse<LightCurveDTO>>> LightCurveSingleGet(int lightCurveId)
    {
        var sr = await _lightCurveService.LightCurveSingleGet(lightCurveId);
        return Ok(sr);
    }

    [HttpGet("{lightCurveId}/values")]
    public async Task<ActionResult<ServiceResponse<string>>> LightCurveSingleValuesGet(int lightCurveId)
    {
        var sr = await _lightCurveService.LightCurveSingleValuesGet(lightCurveId);
        return Ok(sr);
    }

    [HttpGet("{lightCurveId}/file")]
    public async Task<IActionResult> LightCurveDataFileGet(int lightCurveId)
    {
        var sr = await _lightCurveService.LightCurveDataFileGet(lightCurveId);
        return sr is null ? BadRequest(Keywords.NotPublished) : sr;
    }

    [Authorize]
    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<int>>> LightCurvePost(LightCurveAdd lightCurveAdd)
    {
        var sr = await _lightCurveService.LightCurvePost(lightCurveAdd);
        return Ok(sr);
    }

    [HttpGet("logs")]
    public async Task<ActionResult<ServiceResponse<List<ObservationLogDTO>>>> ObservationLogListGet()
    {
        var response = await _lightCurveService.ObservationLogListGet();
        return Ok(response);
    }

    [HttpGet("logs/{id}")]
    public async Task<ActionResult<ServiceResponse<ObservationLogDetailDTO>>> ObservationLogSingleGet(string id)
    {
        var response = await _lightCurveService.ObservationLogSingleGet(id);
        return Ok(response);
    }
}