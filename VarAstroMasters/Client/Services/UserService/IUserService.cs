namespace VarAstroMasters.Client.Services.UserService;

public interface IUserService
{
    Task<UserDTO> GetUserAsync(string userId);
    Task<UserDTO> GetUserFromTokenAsync();
    Task<List<DeviceDTO>> GetMyDevices();
}