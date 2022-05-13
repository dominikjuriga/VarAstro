namespace VarAstroMasters.Server.Services.UserStarIdentificationService;

public interface IUserStarIdentificationService
{
    Task<ServiceResponse<List<UserStarIdentification>>> UserIdentificationsListGet();
}