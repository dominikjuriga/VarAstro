namespace VarAstroMasters.Server.Services.StarService;

public interface IStarService
{
    Task<ServiceResponse<List<StarDTO>>> GetStarsAsync();
    Task<ServiceResponse<StarDTO>> GetStarAsync(int starId);
    Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery);
}