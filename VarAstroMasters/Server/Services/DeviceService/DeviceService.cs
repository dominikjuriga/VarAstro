using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly IAuthService _authService;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public DeviceService(DataContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<Device>> DevicePost(Device device)
    {
        // Verify user that sent the request exists
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<Device>(Keywords.InvalidToken);

        // Add the device to the context
        device.UserId = userId;
        _context.Devices.Add(device);

        // Verify that the changes have been saved
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<Device>(Keywords.PostFailed);

        return new ServiceResponse<Device>
        {
            Data = device,
            Message = Keywords.PostSucceeded
        };
    }

    public async Task<ServiceResponse<List<Device>>> UserFromTokenDevicesGet()
    {
        // Verify user that sent the request exists
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<Device>>(Keywords.InvalidToken);

        // List devices for the user in token
        var devices = await _context.Devices
            .Where(d => d.UserId == userId)
            .ToListAsync();

        return new ServiceResponse<List<Device>>
        {
            Data = devices
        };
    }

    public async Task<ServiceResponse<bool>> DeviceDelete(int deviceId)
    {
        // Check that the device exists
        var dbDevice = await _context.Devices.FindAsync(deviceId);
        if (dbDevice is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        // Check whether there are any light curves associated with the device
        if (_context.LightCurves.Any(lc => lc.DeviceId == deviceId))
            return ResponseHelper.FailResponse<bool>(
                $"{Keywords.DeleteFailed} Toto zariadenie je naviazané na niektoré vaše pozorovania.");

        // If all passed, remove device
        _context.Devices.Remove(dbDevice);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<bool>(Keywords.DeleteFailed);


        return new ServiceResponse<bool>
        {
            Message = Keywords.DeleteSucceeded
        };
    }

    public async Task<ServiceResponse<Device>> DevicePut(Device device)
    {
        // Verify the device exists
        var dbDevice = _context.Devices.Any(d => d.Id == device.Id);
        if (!dbDevice) return ResponseHelper.FailResponse<Device>(Keywords.NotFoundMessage);

        // If it does, update
        _context.Devices.Update(device);

        // verify that the changes have been saved
        var result = await _context.SaveChangesAsync();
        if (result == 0)
            ResponseHelper.FailResponse<DeviceDTO>(Keywords.PutFailed);
        return new ServiceResponse<Device>
        {
            Data = device,
            Message = Keywords.PutSucceeded
        };
    }
}