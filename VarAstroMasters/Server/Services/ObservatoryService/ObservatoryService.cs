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

    public async Task<ServiceResponse<List<ObservatoryDTO>>> UserFromTokenObservatoriesGet()
    {
        var userId = _authService.GetUserId();
        var data = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Observatories)
            .FirstOrDefaultAsync();
        List<ObservatoryDTO> observatories = new();
        foreach (var observatory in data.Observatories)
            observatories.Add(new ObservatoryDTO
            {
                Address = observatory.Address,
                Longitude = observatory.Longitude,
                Latitude = observatory.Latitude,
                Id = observatory.Id
            });
        return new ServiceResponse<List<ObservatoryDTO>>
        {
            Data = observatories
        };
    }

    public async Task<ServiceResponse<bool>> ObservatoryPost(ObservatoryAdd observatoryAdd)
    {
        var userId = _authService.GetUserId();
        var observatory = new Observatory
        {
            Address = observatoryAdd.Address,
            Longitude = observatoryAdd.Longitude,
            Latitude = observatoryAdd.Latitude,
            UserId = userId
        };

        _context.Observatories.Add(observatory);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true
        };
    }

    public async Task<ServiceResponse<bool>> ObservatoryDelete(int observatoryId)
    {
        var dbObservatory = await _context.Observatories.FindAsync(observatoryId);
        if (dbObservatory is null)
            return new ServiceResponse<bool>
            {
                Success = false
            };

        _context.Observatories.Remove(dbObservatory);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true
        };
    }

    public async Task<ServiceResponse<ObservatoryDTO>> ObservatoryEdit(ObservatoryEdit observatoryEdit)
    {
        var dbObservatory = await _context.Observatories.FindAsync(observatoryEdit.Id);
        if (dbObservatory is null)
            return new ServiceResponse<ObservatoryDTO>
            {
                Success = false
            };

        dbObservatory.Address = observatoryEdit.Address;
        dbObservatory.Longitude = observatoryEdit.Longitude;
        dbObservatory.Latitude = observatoryEdit.Latitude;

        await _context.SaveChangesAsync();

        return new ServiceResponse<ObservatoryDTO>
        {
            Data = new ObservatoryDTO
            {
                Address = dbObservatory.Address,
                Longitude = dbObservatory.Longitude,
                Latitude = dbObservatory.Latitude,
                Id = dbObservatory.Id
            }
        };
    }
}