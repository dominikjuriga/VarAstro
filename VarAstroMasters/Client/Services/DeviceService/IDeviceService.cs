namespace VarAstroMasters.Client.Services.DeviceService;

public interface IDeviceService
{
    Task<bool> AddDevice(DeviceAdd deviceAdd);
    Task<List<DeviceDTO>> GetMyDevices();
    Task<bool> DeleteDevice(int deviceId);
    Task<DeviceDTO> EditDevice(DeviceEdit device);
}