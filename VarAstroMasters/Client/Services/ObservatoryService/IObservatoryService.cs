using VarAstroMasters.Client.Pages.Observatories;

namespace VarAstroMasters.Client.Services.ObservatoryService;

public interface IObservatoryService
{
    public Task<ServiceResponse<List<Observatory>>> ObservatoryListFromTokenGet();
    public Task<ServiceResponse<Observatory>> ObservatoryPost(Observatory observatory);
    public Task<ServiceResponse<bool>> ObservatoryDelete(int modelId);
    public Task<ServiceResponse<Observatory>> ObservatoryPut(Observatory observatory);
}