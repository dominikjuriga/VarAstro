namespace VarAstroMasters.Server.Services.DeviceService;

public interface IDeviceService
{
    Task<ServiceResponse<Device>> DevicePost(Device device);
    Task<ServiceResponse<List<Device>>> UserFromTokenDevicesGet();
    Task<ServiceResponse<bool>> DeviceDelete(int deviceId);
    Task<ServiceResponse<Device>> DevicePut(Device device);
}