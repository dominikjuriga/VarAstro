namespace VarAstroMasters.Client.Services.UserService;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDTO> UserSingleGet(string userId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<UserDTO>>($"{Endpoints.ApiUserSingleGet}/{userId}");
        return response.Data;
    }

    public async Task<UserDTO> UserFromTokenGet()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<UserDTO>>(Endpoints.ApiUserFromTokenGet);
        return response.Data;
    }

    public async Task<List<DeviceDTO>> UserMyDevicesGet()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<DeviceDTO>>>(Endpoints.ApiUserMyDevicesGet);
        return response.Data;
    }
}