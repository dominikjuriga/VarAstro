namespace VarAstroMasters.Client.Services.UserStarIdentificationService;

public class UserStarIdentificationService : IUserStarIdentificationService
{
    private readonly HttpClient _http;

    public UserStarIdentificationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<List<UserStarIdentification>>> UserIdentificationsListGet()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<UserStarIdentification>>>(Endpoints
                .ApiUserStarIdentificationsFromToken);
        return response;
    }
}