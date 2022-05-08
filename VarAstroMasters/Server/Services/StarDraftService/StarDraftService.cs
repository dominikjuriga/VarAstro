using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.StarDraftService;

public class StarDraftService : IStarDraftService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public StarDraftService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<int>> DraftPost(StarDraftAdd starDraftAdd)
    {
        var userId = _authService.GetUserId();
        var starDraft = new StarDraft
        {
            Name = starDraftAdd.Name,
            Ra = starDraftAdd.Ra,
            Dec = starDraftAdd.Dec,
            UserId = userId
        };
        _context.StarsDrafts.Add(starDraft);

        await _context.SaveChangesAsync();
        return new ServiceResponse<int>
        {
            Data = starDraft.Id,
            Message = "Používateľská hviezda vytvorená"
        };
    }


    public async Task<ServiceResponse<StarDraft>> DraftSingleGet(int id)
    {
        var sd = await _context.StarsDrafts
            .Where(sd => sd.Id == id)
            .FirstOrDefaultAsync();

        return new ServiceResponse<StarDraft>
        {
            Data = sd
        };
    }

    public async Task<ServiceResponse<List<StarDraft>>> DraftListGet()
    {
        var userId = _authService.GetUserId();
        var sd = await _context.StarsDrafts
            .Where(sd => sd.UserId == userId)
            .ToListAsync();

        return new ServiceResponse<List<StarDraft>>
        {
            Data = sd
        };
    }
}