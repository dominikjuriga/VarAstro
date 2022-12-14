namespace VarAstroMasters.Client.Services.UserService;

public interface IUserService
{
    Task<UserDTO> UserSingleGet(string userId);
    Task<UserDTO> UserFromTokenGet();
    Task<ServiceResponse<List<DeviceDTO>>> UserMyDevicesGet();
}