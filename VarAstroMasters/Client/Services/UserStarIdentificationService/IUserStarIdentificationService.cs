namespace VarAstroMasters.Client.Services.UserStarIdentificationService;

public interface IUserStarIdentificationService
{
    Task<ServiceResponse<List<UserStarIdentificationDTO>>> UserIdentificationsListGet();
    Task<ServiceResponse<bool>> UserIdentificationsPost(UserStarIdentificationCreateDTO usi);
}