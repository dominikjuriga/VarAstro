using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<UserDTO>> GetUserAsync(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            return new ServiceResponse<UserDTO>
            {
                Success = false
            };
        }
        return new ServiceResponse<UserDTO>
        {
            Data = new UserDTO
            {
                Id = user.Id,
                Name = user.UserName
            }
        };
    }
}