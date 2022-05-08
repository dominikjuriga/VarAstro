namespace VarAstroMasters.Server.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<UserDTO>> UserSingleGet(string userId);
    Task<ServiceResponse<UserDTO>> UserSingleFromTokenGet();
    Task<ServiceResponse<List<DeviceDTO>>> UserDeviceListGet();
}