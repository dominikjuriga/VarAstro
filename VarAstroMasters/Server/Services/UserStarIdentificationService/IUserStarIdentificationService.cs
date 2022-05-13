namespace VarAstroMasters.Server.Services.UserStarIdentificationService;

public interface IUserStarIdentificationService
{
    Task<ServiceResponse<List<UserStarIdentificationDTO>>> UserIdentificationsListGet();
    Task<ServiceResponse<bool>> UserIdentificationsPost(UserStarIdentificationCreateDTO usi);
}