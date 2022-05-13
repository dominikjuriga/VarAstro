namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserStarIdentificationController : ControllerBase
{
    private readonly IUserStarIdentificationService _userStarIdentificationService;

    public UserStarIdentificationController(IUserStarIdentificationService userStarIdentificationService)
    {
        _userStarIdentificationService = userStarIdentificationService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<UserStarIdentificationDTO>>>> UserIdentificationsListGet()
    {
        var response = await _userStarIdentificationService.UserIdentificationsListGet();
        return Ok(response);
    }
}