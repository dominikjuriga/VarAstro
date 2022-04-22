

namespace VarAstroMasters.Client.Services.StarService;

public interface IStarService
{
    Task<List<StarDTO>> GetStarsAsync();
    Task<StarDTO> GetStarAsync(int starId);
}