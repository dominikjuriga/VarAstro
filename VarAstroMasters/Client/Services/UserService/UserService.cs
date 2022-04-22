namespace VarAstroMasters.Client.Services.UserService;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<UserDTO> GetUserAsync(string userId)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<UserDTO>>($"{Endpoints.ApiUserGetSingle}/{userId}");
        return response.Data;
    }
}