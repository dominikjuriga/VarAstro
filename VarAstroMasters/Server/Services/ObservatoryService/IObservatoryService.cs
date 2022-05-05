using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Server.Services.ObservatoryService;

public interface IObservatoryService
{
    Task<ServiceResponse<List<Observatory>>> GetMyObservatories();
    Task<ServiceResponse<bool>> AddObservatory(Observatory observatoryAdd);
}