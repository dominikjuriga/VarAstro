namespace VarAstroMasters.Client.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly HttpClient _http;

    public DeviceService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<Device>?> DevicePost(Device deviceAdd)
    {
        var response =
            await _http.PostAsJsonAsync(Endpoints.ApiDevicePost, deviceAdd);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<Device>>();
    }

    public async Task<ServiceResponse<List<Device>>?> DevicesFromTokenGet()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<Device>>>(Endpoints.ApiDeviceMyDevicesGet);
        return response;
    }

    public async Task<ServiceResponse<bool>?> DeviceDelete(int deviceId)
    {
        var response = await _http.DeleteAsync($"{Endpoints.ApiDeviceDelete}/{deviceId}");
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<ServiceResponse<Device>?> DevicePut(Device device)
    {
        var response = await _http.PutAsJsonAsync(Endpoints.ApiDeviceEdit, device);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<Device>>();
    }
}