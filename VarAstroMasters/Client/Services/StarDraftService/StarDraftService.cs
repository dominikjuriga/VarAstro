namespace VarAstroMasters.Client.Services.StarDraftService;

public class StarDraftService : IStarDraftService
{
    private readonly HttpClient _http;

    public StarDraftService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<List<StarDraft>>> DraftListGet()
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<List<StarDraft>>>(Endpoints.ApiStarDraftListGet);
        return response;
    }


    public async Task<ServiceResponse<int>> DraftPost(StarDraftAdd starDraftAdd)
    {
        var response = await _http.PostAsJsonAsync(Endpoints.ApiStarDraftPost, starDraftAdd);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<StarDraft>> DraftSingleGet(int id)
    {
        var response =
            await _http.GetFromJsonAsync<ServiceResponse<StarDraft>>($"{Endpoints.ApiStarDraftSingleGet}/{id}");
        return response;
    }
}