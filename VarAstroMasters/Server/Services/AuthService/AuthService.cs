﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VarAstroMasters.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

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
        User identityUser = new()
        {
            Email = userRegister.EmailAddress,
            UserName = userRegister.EmailAddress,
            FirstName = userRegister.FirstName,
            LastName = userRegister.LastName
        };
        // Attempt to create user from the request data
        var userIdentityResult = await _userManager.CreateAsync(identityUser, userRegister.Password);

        // User exists or other error..
        if (!userIdentityResult.Succeeded)
            return ResponseHelper.FailResponse<string>(
                $"{Keywords.RegisterFailed}:\n{ErrorsToString(userIdentityResult.Errors)}");

        // Add user to role
        var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, Keywords.Role_User);

        if (userIdentityResult.Succeeded && roleIdentityResult.Succeeded)
        {
            var response = new ServiceResponse<string>
            {
                Data = identityUser.Id,
                Message = Keywords.RegisterSucceeded
            };
            return response;
        }

        return ResponseHelper.FailResponse<string>(Keywords.RegisterFailed);
    }

    public async Task<ServiceResponse<string>> LogIn([FromBody] UserLogin userLogin)
    {
        // Check whether the credentials are valid
        var signInResult =
            await _signInManager.PasswordSignInAsync(userLogin.EmailAddress, userLogin.Password, false, false);
        if (signInResult.Succeeded)
        {
            var identityUser = await _userManager.FindByNameAsync(userLogin.EmailAddress);
            var jwtString = await GenerateJsonWebToken(identityUser);
            return new ServiceResponse<string>
            {
                Data = jwtString
            };
        }

        return ResponseHelper.FailResponse<string>(Keywords.InvalidCredentials);
    }

    public string? GetUserId()
    {
        // Find user id that is sent within the HttpContext of the request
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    private async Task<string> GenerateJsonWebToken(User identityUser)
    {
        // Grab private key from configuration
        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration[Keywords.JWT_Key]));
        // Create signing credentials
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

        // Specify claims of the token
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
        };

        // Get user roles
        IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

        // Generate token object
        var token = new JwtSecurityToken(
            _configuration[Keywords.JWT_Issuer],
            _configuration[Keywords.JWT_Issuer],
            claims,
            null,
            DateTime.Now.AddDays(1),
            signingCredentials
        );

        // Write the token into the handler to send to client
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string ErrorsToString(IEnumerable<IdentityError> errors)
    {
        // Loop through errors and add them as lines.
        var result = string.Empty;
        foreach (var error in errors) result += $"[{error.Code}]: {error.Description}\n";

        return result;
    }
}