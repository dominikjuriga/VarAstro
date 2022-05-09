using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Server.Services.ObservatoryService;

public class ObservatoryService : IObservatoryService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public ObservatoryService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<List<Observatory>>> UserFromTokenObservatoriesGet()
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<Observatory>>(Keywords.InvalidToken);

        var data = await _context.Observatories
            .Where(o => o.UserId == userId)
            .ToListAsync();

        return new ServiceResponse<List<Observatory>>
        {
            Data = data
        };
    }

    public async Task<ServiceResponse<Observatory>> ObservatoryPost(Observatory observatory)
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<Observatory>(Keywords.InvalidToken);
        observatory.UserId = userId;

        _context.Observatories.Add(observatory);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<Observatory>(Keywords.PostFailed);


        return new ServiceResponse<Observatory>
        {
            Data = observatory,
            Message = Keywords.PostSucceeded
        };
    }

    public async Task<ServiceResponse<bool>> ObservatoryDelete(int observatoryId)
    {
        var dbObservatory = await _context.Observatories.FindAsync(observatoryId);
        if (dbObservatory is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        _context.Observatories.Remove(dbObservatory);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<bool>(Keywords.DeleteFailed);

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = Keywords.DeleteSucceeded
        };
    }

    public async Task<ServiceResponse<Observatory>> ObservatoryPut(Observatory observatory)
    {
        var dbObservatory = _context.Observatories.Any(o => o.Id == observatory.Id);
        if (!dbObservatory)
            return ResponseHelper.FailResponse<Observatory>(Keywords.NotFoundMessage);

        _context.Observatories.Update(observatory);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<Observatory>(Keywords.PutFailed);

        return new ServiceResponse<Observatory>
        {
            Data = observatory,
            Message = Keywords.PutSucceeded
        };
    }
}