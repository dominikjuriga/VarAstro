using VarAstroMasters.Shared.CompositeKeys;

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

    [HttpGet("publication/{starId:int}")]
    public async Task<ActionResult<ServiceResponse<StarPublish>>> GetPublication(int starId)
    {
        var response = await _starService.GetPublication(starId);
        return Ok(response);
    }

    [HttpPost("publication")]
    public async Task<ActionResult<ServiceResponse<bool>>> SavePublication(StarPublish starPublish)
    {
        var response = await _starService.SavePublication(starPublish);
        return Ok(response);
    }

    [HttpGet("catalogs/{starId:int}")]
    public async Task<ActionResult<ServiceResponse<List<StarCatalog>>>> GetStarCatalogs(int starId)
    {
        var response = await _starService.GetStarCatalogs(starId);
        return Ok(response);
    }

    [HttpDelete("starcatalog/{starId:int}/{catalogId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteStarCatalog(int starId, string catalogId)
    {
        var response = await _starService.DeleteStarCatalog(starId, catalogId);
        return Ok(response);
    }

    [HttpPost("starcatalog")]
    public async Task<ActionResult<ServiceResponse<StarCatalog>>> SaveStarCatalog(StarCatalog starCatalog)
    {
        var response = await _starService.SaveStarCatalog(starCatalog);
        return Ok(response);
    }

    [HttpPost("starcatalog/primary")]
    public async Task<ActionResult<ServiceResponse<StarCatalog>>> SetStarCatalogPrimary(StarCatalogCK identification)
    {
        var response = await _starService.SetStarCatalogPrimary(identification);
        return Ok(response);
    }

    [HttpGet("catalogs")]
    public async Task<ActionResult<ServiceResponse<List<Catalog>>>> GetCatalogs()
    {
        var response = await _starService.GetCatalogs();
        return Ok(response);
    }
}