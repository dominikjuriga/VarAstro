namespace VarAstroMasters.Client.Services.DeviceService;

public interface IDeviceService
{
    Task<ServiceResponse<Device>?> DevicePost(Device deviceAdd);
    Task<ServiceResponse<List<Device>>?> DevicesFromTokenGet();
    Task<ServiceResponse<bool>?> DeviceDelete(int deviceId);
    Task<ServiceResponse<Device>?> DevicePut(Device device);
}