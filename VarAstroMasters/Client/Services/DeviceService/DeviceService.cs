namespace VarAstroMasters.Client.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly HttpClient _http;

    public DeviceService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> DevicePost(DeviceAdd deviceAdd)
    {
        var response =
            await _http.PostAsJsonAsync(
                Endpoints.ApiDevicePost, deviceAdd);
        return response != null;
    }

    public async Task<List<DeviceDTO>> DevicesFromTokenGet()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<DeviceDTO>>>(Endpoints.ApiDeviceMyDevicesGet);
        return response.Data;
    }

    public async Task<bool> DeviceDelete(int deviceId)
    {
        await _http.DeleteAsync($"{Endpoints.ApiDeviceDelete}/{deviceId}");
        return true;
    }

    public async Task<DeviceDTO> DeviceEdit(DeviceEdit device)
    {
        var response = await _http.PutAsJsonAsync(Endpoints.ApiDeviceEdit, device);
        var data = await response.Content.ReadFromJsonAsync<ServiceResponse<DeviceDTO>>();
        return data.Data;
    }
}