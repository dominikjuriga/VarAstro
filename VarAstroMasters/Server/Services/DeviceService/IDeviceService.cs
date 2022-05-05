namespace VarAstroMasters.Server.Services.DeviceService;

public interface IDeviceService
{
    Task<ServiceResponse<bool>> AddDeviceAsync(DeviceAdd deviceAdd);
    Task<ServiceResponse<List<DeviceDTO>>> GetMyDevices();
}