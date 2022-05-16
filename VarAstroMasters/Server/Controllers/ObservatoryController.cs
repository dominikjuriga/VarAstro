using Microsoft.AspNetCore.Authorization;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ObservatoryController : ControllerBase
{
    private readonly IObservatoryService _observatoryService;

    public ObservatoryController(IObservatoryService observatoryService)
    {
        _observatoryService = observatoryService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Observatory>>>> UserFromTokenObservatoriesGet()
    {
        var sr = await _observatoryService.UserFromTokenObservatoriesGet();
        return Ok(sr);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Observatory>>> ObservatoryPost(Observatory observatory)
    {
        var sr = await _observatoryService.ObservatoryPost(observatory);
        return Ok(sr);
    }

    [Authorize]
    [HttpDelete("{observatoryId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> ObservatoryDelete(int observatoryId)
    {
        var sr = await _observatoryService.ObservatoryDelete(observatoryId);
        return Ok(sr);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Observatory>>> ObservatoryPut(Observatory observatory)
    {
        var sr = await _observatoryService.ObservatoryPut(observatory);
        return Ok(sr);
    }
}