namespace VarAstroMasters.Server.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<UserDTO>> GetUserAsync(string userId);
    Task<ServiceResponse<UserDTO>> GetUserFromTokenAsync();
    Task<ServiceResponse<List<DeviceDTO>>> GetUserDevices();
}