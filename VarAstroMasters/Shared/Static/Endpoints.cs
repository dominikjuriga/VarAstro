namespace VarAstroMasters.Shared.Static;

public static class Endpoints
{
    public static readonly string ApiAuthRegister = "api/auth/register";
    public static readonly string ApiAuthLogin = "api/auth/login";

    
    public static readonly string ApiStarBasePath = "api/star";
    public static readonly string ApiStarGetAll = $"{ApiStarBasePath}";
    public static readonly string ApiStarGetSingle = $"{ApiStarBasePath}";
    public static readonly string ApiUploadFile = "api/star/fileUpload";
    
    public static readonly string ClientStarBase = "/stars";
    public static readonly string ClientStarGetAll = $"{ClientStarBase}";
    public static readonly string ClientStarGetSingle = $"{ClientStarBase}";

    public static readonly string ApiLightCurveBasePath = "api/lightcurve";
    public static readonly string ApiLightCurveGetAll = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurveGetSingle = $"{ApiLightCurveBasePath}";

    public static readonly string ClientLightCurveBase = "/LightCurves";
    public static readonly string ClientLightCurveGetAll = $"{ClientLightCurveBase}";
    public static readonly string ClientLightCurveGetSingle = $"{ClientLightCurveBase}";
    
    public static readonly string ClientAuthLogIn = "/login";
    public static readonly string ClientAuthRegister = "/register";
}