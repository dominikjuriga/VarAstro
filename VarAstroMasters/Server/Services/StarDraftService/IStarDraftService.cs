namespace VarAstroMasters.Server.Services.StarDraftService;

public interface IStarDraftService
{
    Task<ServiceResponse<int>> DraftPost(StarDraftAdd starDraftAdd);
    Task<ServiceResponse<StarDraft>> DraftSingleGet(int id);
    Task<ServiceResponse<List<StarDraft>>> DraftListGet();
}