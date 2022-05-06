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

    [HttpGet("search/{searchQuery}")]
    public async Task<ActionResult<ServiceResponse<StarSearchDTO>>> Search(string searchQuery)
    {
        var response = await _starService.Search(searchQuery);
        return Ok(response);
    }
}