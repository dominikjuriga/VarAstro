using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.ObservatoryService;

public class ObservatoryService : IObservatoryService
{
    private readonly IAuthService _authService;
    private readonly DataContext _context;

    public ObservatoryService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<List<Observatory>>> UserFromTokenObservatoriesGet()
    {
        // Get user token from request
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<Observatory>>(Keywords.InvalidToken);

        // Get their observatories
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
        // Get user token from request
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<Observatory>(Keywords.InvalidToken);
        observatory.UserId = userId;

        // save the object and return response
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
        // Find the observatory in the database
        var dbObservatory = await _context.Observatories.FindAsync(observatoryId);
        if (dbObservatory is null)
            return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        // If there are any associated LCs, you cannot remove it
        if (_context.LightCurves.Any(lc => lc.ObservatoryId == observatoryId))
            return ResponseHelper.FailResponse<bool>(
                $"{Keywords.DeleteFailed} Táto hvezdáreň je naviazaná na niektoré vaše pozorovania.");

        // If all else passed, remove and return response
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
        // Find the item in DB
        var dbObservatory = _context.Observatories.Any(o => o.Id == observatory.Id);
        if (!dbObservatory)
            return ResponseHelper.FailResponse<Observatory>(Keywords.NotFoundMessage);

        // If exists, update and return response
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