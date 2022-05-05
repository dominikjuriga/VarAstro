using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public interface IObservatoryService
{
    public Task<List<ObservatoryDTO>> GetMyObservatories();
    public Task<ServiceResponse<bool>> AddObservatory(ObservatoryAdd observatoryAdd);
    public Task<ServiceResponse<bool>> DeleteObservatory(int modelId);
    public Task<ServiceResponse<ObservatoryDTO>> EditObservatory(ObservatoryEdit observatoryEdit);
}