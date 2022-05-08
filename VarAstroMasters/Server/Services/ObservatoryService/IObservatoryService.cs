using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Server.Services.ObservatoryService;

public interface IObservatoryService
{
    Task<ServiceResponse<List<ObservatoryDTO>>> UserFromTokenObservatoriesGet();
    Task<ServiceResponse<bool>> ObservatoryPost(ObservatoryAdd observatoryAdd);
    Task<ServiceResponse<bool>> ObservatoryDelete(int observatoryId);
    Task<ServiceResponse<ObservatoryDTO>> ObservatoryEdit(ObservatoryEdit observatoryEdit);
}