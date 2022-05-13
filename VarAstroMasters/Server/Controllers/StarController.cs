using Microsoft.AspNetCore.Authorization;
using VarAstroMasters.Shared.CompositeKeys;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StarController : ControllerBase
{
    private readonly IStarService _starService;

    public StarController(IStarService starService)
    {
        _starService = starService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<StarDTO>>>> StarListGet()
    {
        var response = await _starService.StarListGet();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> StarPost(NewStar newStar)
    {
        var response = await _starService.StarPost(newStar);
        return Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ServiceResponse<bool>>> StarPut(Star star)
    {
        var response = await _starService.StarPut(star);
        return Ok(response);
    }

    [HttpGet("{starId}")]
    public async Task<ActionResult<ServiceResponse<StarDTO>>> StarSingleGet(int starId)
    {
        var response = await _starService.StarSingleGet(starId);
        return Ok(response);
    }

    [HttpGet("nometa/{starId}")]
    public async Task<ActionResult<ServiceResponse<StarDTO>>> StarSingleWithoutMetaGet(int starId)
    {
        var response = await _starService.StarSingleWithoutMetaGet(starId);
        return Ok(response);
    }

    [HttpGet("search/{searchQuery}")]
    public async Task<ActionResult<ServiceResponse<StarSearchDTO>>> Search(string searchQuery)
    {
        var response = await _starService.Search(searchQuery);
        return Ok(response);
    }

    [HttpGet("searchbycoords/{searchQuery}")]
    public async Task<ActionResult<ServiceResponse<StarSearchDTO>>> SearchByCoords(string searchQuery)
    {
        var response = await _starService.SearchByCoords(searchQuery);
        return Ok(response);
    }

    [HttpPost("firstbycoords")]
    public async Task<ActionResult<ServiceResponse<StarSearchDTO>>> FirstByCoords(StarCoordDTO coords)
    {
        var response = await _starService.FirstByCoords(coords);
        return Ok(response);
    }

    [HttpGet("publication/{starId:int}")]
    public async Task<ActionResult<ServiceResponse<StarPublish>>> PublicationSingleGet(int starId)
    {
        var response = await _starService.PublicationSingleGet(starId);
        return Ok(response);
    }

    [HttpPost("publication")]
    public async Task<ActionResult<ServiceResponse<StarPublish>>> PublicationPost(StarPublish starPublish)
    {
        var response = await _starService.PublicationPost(starPublish);
        return Ok(response);
    }

    [HttpGet("catalogs/{starId:int}")]
    public async Task<ActionResult<ServiceResponse<List<StarCatalog>>>> StarCatalogListForStarSingleGet(int starId)
    {
        var response = await _starService.StarCatalogListForStarSingleGet(starId);
        return Ok(response);
    }

    [HttpDelete("starcatalog/{starId:int}/{catalogId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> StarCatalogDelete(int starId, string catalogId)
    {
        var response = await _starService.StarCatalogDelete(starId, catalogId);
        return Ok(response);
    }

    [HttpPost("starcatalog")]
    public async Task<ActionResult<ServiceResponse<StarCatalog>>> StarCatalogPost(StarCatalog starCatalog)
    {
        var response = await _starService.StarCatalogPost(starCatalog);
        return Ok(response);
    }

    [HttpPut("starcatalog")]
    public async Task<ActionResult<ServiceResponse<StarCatalog>>> StarCatalogPut(StarCatalog starCatalog)
    {
        var response = await _starService.StarCatalogPut(starCatalog);
        return Ok(response);
    }

    [HttpPost("starcatalog/primary")]
    public async Task<ActionResult<ServiceResponse<StarCatalog>>> StarCatalogSetAsPrimaryPost(
        StarCatalogCK identification)
    {
        var response = await _starService.StarCatalogSetAsPrimaryPost(identification);
        return Ok(response);
    }

    [HttpGet("catalogs")]
    public async Task<ActionResult<ServiceResponse<List<Catalog>>>> CatalogListGet()
    {
        var response = await _starService.CatalogListGet();
        return Ok(response);
    }

    [HttpDelete("catalog/{catalogName}")]
    public async Task<ActionResult<ServiceResponse<bool>>> CatalogDelete(string catalogName)
    {
        var response = await _starService.CatalogDelete(catalogName);
        return Ok(response);
    }

    [HttpPost("catalog")]
    public async Task<ActionResult<ServiceResponse<Catalog>>> CatalogPost(Catalog catalog)
    {
        var response = await _starService.CatalogPost(catalog);
        return Ok(response);
    }
}