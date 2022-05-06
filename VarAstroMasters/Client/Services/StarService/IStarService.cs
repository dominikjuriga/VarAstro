namespace VarAstroMasters.Client.Services.StarService;

public interface IStarService
{
    Task<List<StarDTO>> GetStarsAsync();
    Task<ServiceResponse<StarDTO>> GetStarAsync(int starId);
}