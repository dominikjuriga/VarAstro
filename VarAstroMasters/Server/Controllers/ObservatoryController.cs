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
    public async Task<ActionResult<ServiceResponse<List<Observatory>>>> GetMyObservatories()
    {
        var sr = await _observatoryService.GetMyObservatories();
        return Ok(sr);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> AddObservatory(Observatory observatoryAdd)
    {
        var sr = await _observatoryService.AddObservatory(observatoryAdd);
        return Ok(sr);
    }
}