using UserStarIdentificationDTO = VarAstroMasters.Shared.DTO.UserStarIdentificationDTO;

namespace VarAstroMasters.Client.Services.UserStarIdentificationService;

public class UserStarIdentificationService : IUserStarIdentificationService
{
    private readonly HttpClient _http;

    public UserStarIdentificationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<List<UserStarIdentificationDTO>>> UserIdentificationsListGet()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<UserStarIdentificationDTO>>>(Endpoints
                .ApiUserStarIdentificationsFromToken);
        return response;
    }
}