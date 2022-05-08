namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ServiceResponse<UserDTO>>> UserSingleGet(string userId)
    {
        var sr = await _userService.UserSingleGet(userId);
        return Ok(sr);
    }

    [HttpGet("token")]
    public async Task<ActionResult<ServiceResponse<UserDTO>>> UserSingleFromTokenGet()
    {
        var sr = await _userService.UserSingleFromTokenGet();
        return Ok(sr);
    }

    [HttpGet("devices")]
    public async Task<ActionResult<ServiceResponse<UserDTO>>> UserDeviceListGet()
    {
        var sr = await _userService.UserDeviceListGet();
        return Ok(sr);
    }
}