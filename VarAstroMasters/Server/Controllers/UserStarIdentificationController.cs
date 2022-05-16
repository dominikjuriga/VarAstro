using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<UserStarIdentificationDTO>>>> UserIdentificationsListGet()
    {
        var response = await _userStarIdentificationService.UserIdentificationsListGet();
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> UserIdentificationsPost(UserStarIdentificationCreateDTO usi)
    {
        var response = await _userStarIdentificationService.UserIdentificationsPost(usi);
        return Ok(response);
    }
}