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
    public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUserAsync(string userId)
    {
        var sr = await _userService.GetUserAsync(userId);
        return Ok(sr);
    }

    [HttpGet("token")]
    public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUserFromTokenAsync()
    {
        var sr = await _userService.GetUserFromTokenAsync();
        return Ok(sr);
    }

    [HttpGet("devices")]
    public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUserDevices()
    {
        var sr = await _userService.GetUserDevices();
        return Ok(sr);
    }
}