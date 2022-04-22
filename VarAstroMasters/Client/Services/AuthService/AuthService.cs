using System.Net.Http.Json;

namespace VarAstroMasters.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<string>> Register(UserRegister userRegister)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiAuthRegister, userRegister);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<string>?> LogIn(UserLogin userLogin)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiAuthLogin, userLogin);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }
}