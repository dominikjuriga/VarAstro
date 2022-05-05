using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Server.Services.ObservatoryService;

public interface IObservatoryService
{
    Task<ServiceResponse<List<ObservatoryDTO>>> GetMyObservatories();
    Task<ServiceResponse<bool>> AddObservatory(ObservatoryAdd observatoryAdd);
    Task<ServiceResponse<bool>> DeleteObservatory(int observatoryId);
    Task<ServiceResponse<ObservatoryDTO>> EditObservatory(ObservatoryEdit observatoryEdit);
}