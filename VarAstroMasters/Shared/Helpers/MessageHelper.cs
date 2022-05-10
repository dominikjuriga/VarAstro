using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Helpers;

public static class MessageHelper
{
    public static string GetAlertType(bool? value)
    {
        return value is not null && value is true ? Keywords.AlertSuccess : Keywords.AlertDanger;
    }
}