// namespace VarAstroMasters.Server.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class StarDraftController : ControllerBase
// {
//     private readonly IStarDraftService _starDraftService;
//
//     public StarDraftController(IStarDraftService starDraftService)
//     {
//         _starDraftService = starDraftService;
//     }
//
//     [HttpPost("draft")]
//     public async Task<ActionResult<ServiceResponse<int>>> DraftPost(StarDraftAdd starDraftAdd)
//     {
//         var response = await _starDraftService.DraftPost(starDraftAdd);
//         return Ok(response);
//     }
//
//
//     [HttpGet("draft/{id:int}")]
//     public async Task<ActionResult<ServiceResponse<int>>> DraftSingleGet(int id)
//     {
//         var response = await _starDraftService.DraftSingleGet(id);
//         return Ok(response);
//     }
//
//     [HttpGet("draft")]
//     public async Task<ActionResult<ServiceResponse<StarDraft>>> DraftListGet()
//     {
//         var response = await _starDraftService.DraftListGet();
//         return Ok(response);
//     }
// }

