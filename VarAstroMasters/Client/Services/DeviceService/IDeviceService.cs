namespace VarAstroMasters.Client.Services.DeviceService;

public interface IDeviceService
{
    Task<bool> DevicePost(DeviceAdd deviceAdd);
    Task<List<DeviceDTO>> DevicesFromTokenGet();
    Task<bool> DeviceDelete(int deviceId);
    Task<DeviceDTO> DeviceEdit(DeviceEdit device);
}