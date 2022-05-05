﻿namespace VarAstroMasters.Client.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly HttpClient _http;

    public DeviceService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> AddDevice(DeviceAdd deviceAdd)
    {
        var response =
            await _http.PostAsJsonAsync(
                Endpoints.ApiDeviceAdd, deviceAdd);
        return response != null;
    }

    public async Task<List<DeviceDTO>> GetMyDevices()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<List<DeviceDTO>>>(Endpoints.ApiDeviceGetMyDevices);
        return response.Data;
    }

    public async Task<bool> DeleteDevice(int deviceId)
    {
        await _http.DeleteAsync($"{Endpoints.ApiDeviceDelete}/{deviceId}");
        return true;
    }

    public async Task<DeviceDTO> EditDevice(Device device)
    {
        var response = await _http.PutAsJsonAsync(Endpoints.ApiDeviceEdit, device);
        var data = await response.Content.ReadFromJsonAsync<ServiceResponse<DeviceDTO>>();
        return data.Data;
    }
}