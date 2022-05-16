namespace VarAstroMasters.Server.Services.ObservatoryService;

public interface IObservatoryService
{
    Task<ServiceResponse<List<Observatory>>> UserFromTokenObservatoriesGet();
    Task<ServiceResponse<Observatory>> ObservatoryPost(Observatory observatory);
    Task<ServiceResponse<bool>> ObservatoryDelete(int observatoryId);
    Task<ServiceResponse<Observatory>> ObservatoryPut(Observatory observatory);
}