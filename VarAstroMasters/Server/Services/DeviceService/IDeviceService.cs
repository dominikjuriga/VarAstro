namespace VarAstroMasters.Server.Services.DeviceService;

public interface IDeviceService
{
    Task<ServiceResponse<bool>> AddDeviceAsync(DeviceAdd deviceAdd);
    Task<ServiceResponse<List<DeviceDTO>>> GetMyDevices();
    Task<ServiceResponse<bool>> DeleteDevice(int deviceId);
    Task<ServiceResponse<DeviceDTO>> EditDevice(DeviceEdit device);
}