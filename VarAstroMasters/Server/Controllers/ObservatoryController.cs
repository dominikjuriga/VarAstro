using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<ServiceResponse<List<ObservatoryDTO>>>> UserFromTokenObservatoriesGet()
    {
        var sr = await _observatoryService.UserFromTokenObservatoriesGet();
        return Ok(sr);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> ObservatoryPost(ObservatoryAdd observatoryAdd)
    {
        var sr = await _observatoryService.ObservatoryPost(observatoryAdd);
        return Ok(sr);
    }

    [HttpDelete("{observatoryId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> ObservatoryDelete(int observatoryId)
    {
        var sr = await _observatoryService.ObservatoryDelete(observatoryId);
        return Ok(sr);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<ObservatoryDTO>>> ObservatoryEdit(ObservatoryEdit observatoryEdit)
    {
        var sr = await _observatoryService.ObservatoryEdit(observatoryEdit);
        return Ok(sr);
    }
}