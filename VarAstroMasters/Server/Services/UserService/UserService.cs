using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public UserService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<UserDTO>> GetUserAsync(string userId)
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

    public async Task<ServiceResponse<UserDTO>> GetUserFromTokenAsync()
    {
        var userId = _authService.GetUserId();
        if (userId is null)
            return new ServiceResponse<UserDTO>
            {
                Success = false
            };
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

        List<DeviceDTO> devices = new();
        foreach (var device in user.Devices)
            devices.Add(new DeviceDTO
            {
                Id = device.Id,
                Name = device.Name
            });
        response.Data.Devices = devices;

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

    public async Task<ServiceResponse<List<DeviceDTO>>> GetUserDevices()
    {
        var userId = _authService.GetUserId();
        if (userId is null)
            return new ServiceResponse<List<DeviceDTO>>
            {
                Success = false
            };

        var devices = await _context.Devices
            .Where(d => d.UserId == userId)
            .ToListAsync();

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