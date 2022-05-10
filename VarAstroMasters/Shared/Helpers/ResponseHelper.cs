using VarAstroMasters.Shared.Responses;

namespace VarAstroMasters.Shared.Helpers;

public static class ResponseHelper
{
    public static ServiceResponse<T> FailResponse<T>(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = message
        };
    }
}