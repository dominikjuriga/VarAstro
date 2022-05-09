using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VarAstroMasters.Server.Services.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public DeviceService(DataContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<Device>> DevicePost(Device device)
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<Device>(Keywords.InvalidToken);

        device.UserId = userId;
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return new ServiceResponse<Device>
        {
            Data = device,
            Message = Keywords.PostSucceeded
        };
    }

    public async Task<ServiceResponse<List<Device>>> UserFromTokenDevicesGet()
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<Device>>(Keywords.InvalidToken);

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
        var dbDevice = await _context.Devices.FindAsync(deviceId);
        if (dbDevice is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        _context.Devices.Remove(dbDevice);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Message = Keywords.DeleteSucceeded
        };
    }

    public async Task<ServiceResponse<Device>> DevicePut(Device device)
    {
        var dbDevice = _context.Devices.Any(d => d.Id == device.Id);
        if (!dbDevice) return ResponseHelper.FailResponse<Device>(Keywords.NotFoundMessage);

        _context.Devices.Update(device);
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