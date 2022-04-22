using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Client.Providers;

public class AppAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public AppAuthStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Attempt to load JWT token from local storage
            string savedToken = await _localStorageService.GetItemAsync<string>(Keywords.JWT_Bearer_Token);
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                // No token is present, therefore user is not logged in
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // User has a token in local storage
            JwtSecurityToken jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            DateTime expiration = jwtSecurityToken.ValidTo;

            if (expiration < DateTime.UtcNow)
            {
                // Token in local storage has expired, therefore user is not logged in
                await _localStorageService.RemoveItemAsync(Keywords.JWT_Bearer_Token);
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Get Claims from token and build auth user object
            var claims = ParseClaims(jwtSecurityToken);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, Keywords.AuthType));

            return new AuthenticationState(user);
        }
        catch (Exception)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    private IList<Claim> ParseClaims(JwtSecurityToken jwtSecurityToken)
    {
        IList<Claim> claims = jwtSecurityToken.Claims.ToList();

        // the value of tokenContent.Subject is the users' email

        claims.Add(new Claim(ClaimTypes.Name, jwtSecurityToken.Subject));

        return claims;
    }

    internal async Task LogIn()
    {
        // Retrieve token from local storage
        string savedToken = await _localStorageService.GetItemAsync<string>(Keywords.JWT_Bearer_Token);
        JwtSecurityToken jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = ParseClaims(jwtSecurityToken);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, Keywords.AuthType));

        Task<AuthenticationState> authenticationState = Task.FromResult(new AuthenticationState(user));

        NotifyAuthenticationStateChanged(authenticationState);
    }

    internal void LogOut()
    {
        ClaimsPrincipal nobody = new ClaimsPrincipal(new ClaimsIdentity());
        Task<AuthenticationState> authenticationState = Task.FromResult(new AuthenticationState(nobody));

        NotifyAuthenticationStateChanged(authenticationState);
    }
}