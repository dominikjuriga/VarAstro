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

    [HttpPost("draft")]
    public async Task<ActionResult<ServiceResponse<int>>> CreateDraft(StarDraftAdd starDraftAdd)
    {
        var response = await _starService.CreateDraft(starDraftAdd);
        return Ok(response);
    }

    [HttpGet("draft/{id:int}")]
    public async Task<ActionResult<ServiceResponse<int>>> GetDraft(int id)
    {
        var response = await _starService.GetDraft(id);
        return Ok(response);
    }

    [HttpGet("draft")]
    public async Task<ActionResult<ServiceResponse<StarDraft>>> GetDraftList()
    {
        var response = await _starService.GetDraftList();
        return Ok(response);
    }

    [HttpGet("logs")]
    public async Task<ActionResult<ServiceResponse<List<ObservationLogDTO>>>> GetObservationLogList()
    {
        var response = await _starService.GetObservationLogList();
        return Ok(response);
    }

    [HttpGet("logs/{id}")]
    public async Task<ActionResult<ServiceResponse<ObservationLogDetailDTO>>> GetObservationLog(string id)
    {
        var response = await _starService.GetObservationLog(id);
        return Ok(response);
    }

    [HttpGet("search/{searchQuery}")]
    public async Task<ActionResult<ServiceResponse<StarSearchDTO>>> Search(string searchQuery)
    {
        var response = await _starService.Search(searchQuery);
        return Ok(response);
    }
}