namespace VarAstroMasters.Server.Services.UserService;
public interface IUserService
{
    Task<ServiceResponse<UserDTO>> GetUserAsync(string userId);
}