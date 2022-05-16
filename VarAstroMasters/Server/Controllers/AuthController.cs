namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegister userRegister)
    {
        var response = await _authService.Register(userRegister);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> LogIn(UserLogin userLogin)
    {
        var response = await _authService.LogIn(userLogin);
        return Ok(response);
    }
}