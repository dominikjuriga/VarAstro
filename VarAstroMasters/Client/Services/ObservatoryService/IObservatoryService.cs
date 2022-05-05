using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public interface IObservatoryService
{
    public Task<List<Observatory>> GetMyObservatories();
    public Task<bool> AddObservatory(Observatory observatoryAdd);
}