namespace VarAstroMasters.Shared.Helpers;

public static class PublishHelper
{
    public static List<PublishVariant> MapPermissions = new()
        { PublishVariant.All, PublishVariant.OnlyMap, PublishVariant.OnlyMapAndCurve };

    public static List<PublishVariant> CurvePermissions = new()
        { PublishVariant.All, PublishVariant.OnlyMapAndCurve };

    public static List<PublishVariant> FilePermissions = new()
        { PublishVariant.All };

    public static bool IsPublic(PublishVariant variant)
    {
        return variant != PublishVariant.None;
    }

    public static bool CanShareMap(PublishVariant variant)
    {
        return MapPermissions.Contains(variant);
    }

    public static bool CanShareCurve(PublishVariant variant)
    {
        return CurvePermissions.Contains(variant);
    }

    public static bool CanShareFile(PublishVariant variant)
    {
        return FilePermissions.Contains(variant);
    }
}