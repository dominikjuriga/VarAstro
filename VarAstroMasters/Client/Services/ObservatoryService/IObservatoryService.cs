using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public interface IObservatoryService
{
    public Task<List<ObservatoryDTO>> ObservatoryListFromTokenGet();
    public Task<ServiceResponse<bool>> ObservatoryPost(ObservatoryAdd observatoryAdd);
    public Task<ServiceResponse<bool>> ObservatoryDelete(int modelId);
    public Task<ServiceResponse<ObservatoryDTO>> ObservatoryEdit(ObservatoryEdit observatoryEdit);
}