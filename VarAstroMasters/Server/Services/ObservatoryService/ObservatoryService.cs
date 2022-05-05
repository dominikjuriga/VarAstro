using Microsoft.EntityFrameworkCore;
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

    public async Task<ServiceResponse<List<Observatory>>> GetMyObservatories()
    {
        var userId = _authService.GetUserId();
        var data = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Observatories)
            .FirstOrDefaultAsync();
        return new ServiceResponse<List<Observatory>>
        {
            Data = data.Observatories
        };
    }

    public async Task<ServiceResponse<bool>> AddObservatory(Observatory observatoryAdd)
    {
        var observatory = new Observatory
        {
            Address = observatoryAdd.Address,
            Longitude = observatoryAdd.Longitude,
            Latitude = observatoryAdd.Latitude
        };

        _context.Observatories.Add(observatory);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true
        };
    }
}