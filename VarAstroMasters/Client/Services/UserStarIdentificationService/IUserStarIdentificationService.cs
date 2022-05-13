using UserStarIdentificationDTO = VarAstroMasters.Shared.DTO.UserStarIdentificationDTO;

namespace VarAstroMasters.Client.Services.UserStarIdentificationService;

public interface IUserStarIdentificationService
{
    Task<ServiceResponse<List<UserStarIdentificationDTO>>> UserIdentificationsListGet();
}