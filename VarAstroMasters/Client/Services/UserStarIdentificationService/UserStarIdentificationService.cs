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

    public async Task<ServiceResponse<bool>> UserIdentificationsPost(UserStarIdentificationCreateDTO usi)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiUserStarIdentificationPost, usi);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }
}