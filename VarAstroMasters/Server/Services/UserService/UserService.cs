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
        return response;
    }
}