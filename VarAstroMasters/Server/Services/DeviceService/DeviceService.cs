using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public DeviceService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<bool>> AddDeviceAsync(DeviceAdd deviceAdd)
    {
        var userId = _authService.GetUserId();
        var newDevice = new Device
        {
            Name = deviceAdd.Name,
            UserId = userId
        };
        var result = _context.Devices.Add(newDevice);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
        };
    }

    public async Task<ServiceResponse<List<DeviceDTO>>> GetMyDevices()
    {
        var userId = _authService.GetUserId();
        var devices = await _context.Devices
            .Where(d => d.UserId == userId)
            .ToListAsync();

        var response = new List<DeviceDTO>
        {
        };

        foreach (var device in devices)
            response.Add(new DeviceDTO
            {
                Id = device.Id,
                Name = device.Name
            });

        return new ServiceResponse<List<DeviceDTO>>
        {
            Data = response
        };
    }
}