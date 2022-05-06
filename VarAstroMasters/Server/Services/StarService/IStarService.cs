namespace VarAstroMasters.Server.Services.StarService;

public interface IStarService
{
    Task<ServiceResponse<List<StarDTO>>> GetStarsAsync();
    Task<ServiceResponse<StarDTO>> GetStarAsync(int starId);
    Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery);
    Task<ServiceResponse<int>> CreateDraft(StarDraftAdd starDraftAdd);
    Task<ServiceResponse<StarDraft>> GetDraft(int id);
    Task<ServiceResponse<List<StarDraft>>> GetDraftList();
}