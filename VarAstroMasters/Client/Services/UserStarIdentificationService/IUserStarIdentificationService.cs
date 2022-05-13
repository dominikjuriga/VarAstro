namespace VarAstroMasters.Client.Services.UserStarIdentificationService;

public interface IUserStarIdentificationService
{
    Task<ServiceResponse<List<UserStarIdentification>>> UserIdentificationsListGet();
}