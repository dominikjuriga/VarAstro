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
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<UserDTO>>($"{Endpoints.ApiUserGetSingle}/{userId}");
        return response.Data;
    }

    public async Task<UserDTO> GetUserFromTokenAsync()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<UserDTO>>(Endpoints.ApiUserGetFromToken);
        return response.Data;
    }

    public async Task<List<DeviceDTO>> GetMyDevices()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<DeviceDTO>>>(Endpoints.ApiUserGetMyDevices);
        return response.Data;
    }
}