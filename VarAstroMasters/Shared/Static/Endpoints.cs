namespace VarAstroMasters.Shared.Static;

public static class Endpoints
{
    public static readonly string ApiAuthRegister = "api/auth/register";
    public static readonly string ApiAuthLogin = "api/auth/login";

    public static readonly string ApiUserBase = "api/user";
    public static readonly string ApiUserGetSingle = $"{ApiUserBase}";
    public static readonly string ApiUserGetFromToken = $"{ApiUserBase}/token";
    public static readonly string ApiUserGetMyDevices = $"{ApiUserBase}/devices";

    public static readonly string ApiLightCurveBasePath = "api/lightcurve";
    public static readonly string ApiLightCurveGetAll = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurveGetSingle = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurveGetValues = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurveAdd = $"{ApiLightCurveBasePath}/add";
    public static readonly string ApiDeviceBasePath = "api/device";
    public static readonly string ApiDeviceAdd = $"{ApiDeviceBasePath}/add";
    public static readonly string ApiDeviceEdit = $"{ApiDeviceBasePath}";
    public static readonly string ApiDeviceDelete = $"{ApiDeviceBasePath}";
    public static readonly string ApiDeviceGetMyDevices = $"{ApiDeviceBasePath}/list";

    public static readonly string ApiStarBasePath = "api/star";
    public static readonly string ApiStarGetAll = $"{ApiStarBasePath}";
    public static readonly string ApiStarGetSingle = $"{ApiStarBasePath}";
    public static readonly string ApiStarSearch = $"{ApiStarBasePath}/search";

    public static readonly string ApiObservatoryBase = "api/observatory";
    public static readonly string ApiObservatoryGetObservatories = $"{ApiObservatoryBase}";
    public static readonly string ApiObservatoryAdd = $"{ApiObservatoryBase}";
    public static readonly string ApiObservatoryDelete = $"{ApiObservatoryBase}";
    public static readonly string ApiObservatoryEdit = $"{ApiObservatoryBase}";


    public static readonly string ClientStarBase = "/Stars";
    public static readonly string ClientStarGetAll = $"{ClientStarBase}";
    public static readonly string ClientStarGetSingle = $"{ClientStarBase}";

    public static readonly string ClientObservatoriesBase = "/Observatories";
    public static readonly string ClientObservatoriesList = $"{ClientObservatoriesBase}";
    public static readonly string ClientObservatoriesAdd = $"{ClientObservatoriesBase}/Add";

    public static readonly string ClientLightCurveBase = "/LightCurves";
    public static readonly string ClientLightCurveGetAll = $"{ClientLightCurveBase}";
    public static readonly string ClientLightCurveGetSingle = $"{ClientLightCurveBase}";
    public static readonly string ClientLightCurveAdd = $"{ClientLightCurveBase}/Add";

    public static readonly string ClientUserBase = "/User";
    public static readonly string ClientUserGetSingle = $"{ClientUserBase}";
    public static readonly string ClientUserProfile = "/me";

    public static readonly string ClientDeviceBase = "/Device";
    public static readonly string ClientDeviceAdd = $"{ClientDeviceBase}/Add";
    public static readonly string ClientDeviceList = $"{ClientDeviceBase}/List";

    public static readonly string ClientAuthLogIn = "/Login";
    public static readonly string ClientAuthRegister = "/Register";

    public static readonly string ClientSearch = "/Search";
    public static readonly string ClientAdmin = "/Admin";
}