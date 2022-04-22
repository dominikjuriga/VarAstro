using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VarAstroMasters.Server.Data;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace VarAstroMasters.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager,
        IConfiguration configuration, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<string>> Register([FromBody] UserRegister userRegister)
    {
        string username = userRegister.EmailAddress;
        string password = userRegister.Password;

        User identityUser = new()
        {
            UserName = username,
            Email = username
        };

        IdentityResult userIdentityResult = await _userManager.CreateAsync(identityUser, password);
        if (!userIdentityResult.Succeeded)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Could not create"
            };
        }

        IdentityResult roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, Keywords.Role_User);

        if (userIdentityResult.Succeeded && roleIdentityResult.Succeeded)
        {
            var response = new ServiceResponse<string>
            {
                Data = identityUser.Id,
                Success = true,
                Message = "Registration Successful."
            };
            return response;
        }


        return new ServiceResponse<string>
        {
            Success = false,
            Message = "Registration failed."
        };
    }

    public async Task<ServiceResponse<string>> LogIn([FromBody] UserLogin userLogin)
    {
        string username = userLogin.EmailAddress;
        string password = userLogin.Password;

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
        if (signInResult.Succeeded)
        {
            User identityUser = await _userManager.FindByNameAsync(username);
            string jwtString = await GenerateJsonWebToken(identityUser);
            return new ServiceResponse<string>
            {
                Data = jwtString
            };
        }

        return new ServiceResponse<string>
        {
            Success = false,
            Message = "Invalid Credentials."
        };
    }

    private async Task<string> GenerateJsonWebToken(User identityUser)
    {
        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration[Keywords.JWT_Key]));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
        };

        IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

        JwtSecurityToken token = new JwtSecurityToken(
            _configuration[Keywords.JWT_Issuer],
            _configuration[Keywords.JWT_Issuer],
            claims,
            null,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
}