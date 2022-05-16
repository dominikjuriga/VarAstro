using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.UserService;

public class UserService : IUserService
{
    private readonly IAuthService _authService;
    private readonly DataContext _context;

    public UserService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<UserDTO>> UserSingleGet(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            return new ServiceResponse<UserDTO>
            {
                Success = false
            };
        return new ServiceResponse<UserDTO>
        {
            Data = new UserDTO
            {
                Id = user.Id,
                Name = user.UserName
            }
        };
    }

    public async Task<ServiceResponse<UserDTO>> UserSingleFromTokenGet()
    {
        // Get user token
        var userId = _authService.GetUserId();
        if (userId is null)
            return new ServiceResponse<UserDTO>
            {
                Success = false
            };

        // Find user in DB
        var user = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Devices)
            .Include(u => u.Observatories)
            .FirstOrDefaultAsync();

        var response = new ServiceResponse<UserDTO>
        {
            Data = new UserDTO
            {
                Id = user.Id,
                Name = user.UserName
            }
        };

        // Map devices to DTOs
        List<DeviceDTO> devices = new();
        foreach (var device in user.Devices)
            devices.Add(new DeviceDTO
            {
                Id = device.Id,
                Name = device.Name
            });
        response.Data.Devices = devices;

        // Map observatories to DTOs
        List<ObservatoryDTO> observatories = new();
        foreach (var observatory in user.Observatories)
            observatories.Add(new ObservatoryDTO
            {
                Address = observatory.Address,
                Longitude = observatory.Longitude,
                Latitude = observatory.Latitude,
                Id = observatory.Id
            });
        response.Data.Observatories = observatories;
        return response;
    }

    public async Task<ServiceResponse<List<DeviceDTO>>> UserDeviceListGet()
    {
        // Find user token
        var userId = _authService.GetUserId();
        if (userId is null)
            return ResponseHelper.FailResponse<List<DeviceDTO>>(Keywords.InvalidToken);

        // Find their devices
        var devices = await _context.Devices
            .Where(d => d.UserId == userId)
            .ToListAsync();

        // Map models to DTOs
        List<DeviceDTO> deviceDTOs = new();

        foreach (var device in devices)
            deviceDTOs.Add(new DeviceDTO
            {
                Id = device.Id,
                Name = device.Name
            });

        return new ServiceResponse<List<DeviceDTO>>
        {
            Data = deviceDTOs
        };
    }
}