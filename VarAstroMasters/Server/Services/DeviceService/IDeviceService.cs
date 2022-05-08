namespace VarAstroMasters.Server.Services.DeviceService;

public interface IDeviceService
{
    Task<ServiceResponse<bool>> DevicePost(DeviceAdd deviceAdd);
    Task<ServiceResponse<List<DeviceDTO>>> UserFromTokenDevicesGet();
    Task<ServiceResponse<bool>> DeviceDelete(int deviceId);
    Task<ServiceResponse<DeviceDTO>> DeviceEdit(DeviceEdit device);
}