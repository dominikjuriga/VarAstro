﻿namespace VarAstroMasters.Client.Services.DeviceService;

public interface IDeviceService
{
    Task<bool> AddDevice(DeviceAdd deviceAdd);
    Task<List<DeviceDTO>> GetMyDevices();
}