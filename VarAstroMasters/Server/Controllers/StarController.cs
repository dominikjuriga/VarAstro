using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
using VarAstroMasters.Shared.DTO;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StarController : ControllerBase
{
    private readonly IStarService _starService;
    private readonly IWebHostEnvironment _env;

    public StarController(IStarService starService, IWebHostEnvironment env)
    {
        _starService = starService;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<StarDTO>>>> GetStarsAsync()
    {
        var response = await _starService.GetStarsAsync();
        return Ok(response);
    }

    [HttpGet("{starId}")]
    public async Task<ActionResult<ServiceResponse<StarDTO>>> GetStarAsync(int starId)
    {
        var response = await _starService.GetStarAsync(starId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<StarDTO>>>> Search(string query)
    {
        var response = await _starService.Search(query);
        return Ok(response);
    }
}