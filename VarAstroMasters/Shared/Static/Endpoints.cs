namespace VarAstroMasters.Shared.Static;

public static class Endpoints
{
    // Naming scheme: 
    // [API/Client]
    // [Domain Model e.g. Star]
    // [Multiplier? e.g. List, Single]
    // [Method e.g. GET, POST]

    //      ---     API     ---

    // Auth
    public static readonly string ApiAuthRegister = "api/auth/register";
    public static readonly string ApiAuthLogin = "api/auth/login";

    // User
    public static readonly string ApiUserBase = "api/user";
    public static readonly string ApiUserSingleGet = $"{ApiUserBase}";
    public static readonly string ApiUserFromTokenGet = $"{ApiUserBase}/token";
    public static readonly string ApiUserMyDevicesGet = $"{ApiUserBase}/devices";

    // Light Curves
    public static readonly string ApiLightCurveBasePath = "api/lightcurve";
    public static readonly string ApiLightCurveListGet = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurveSingleGet = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurveSingleValuesGet = $"{ApiLightCurveBasePath}";
    public static readonly string ApiLightCurvePost = $"{ApiLightCurveBasePath}/add";

    // Devices
    public static readonly string ApiDeviceBasePath = "api/device";
    public static readonly string ApiDevicePost = $"{ApiDeviceBasePath}/add";
    public static readonly string ApiDeviceEdit = $"{ApiDeviceBasePath}";
    public static readonly string ApiDeviceDelete = $"{ApiDeviceBasePath}";
    public static readonly string ApiDeviceMyDevicesGet = $"{ApiDeviceBasePath}/list";

    // Stars
    public static readonly string ApiStarBasePath = "api/star";
    public static readonly string ApiStarAllGet = $"{ApiStarBasePath}";
    public static readonly string ApiStarSingleGet = $"{ApiStarBasePath}";
    public static readonly string ApiStarSearch = $"{ApiStarBasePath}/search";
    public static readonly string ApiStarPublicationGet = $"{ApiStarBasePath}/publication";

    // Star Drafts
    public static readonly string ApiStarDraftBasePath = "api/stardraft";

    public static readonly string ApiStarDraftSingleGet = $"{ApiStarDraftBasePath}/draft";
    public static readonly string ApiStarDraftListGet = $"{ApiStarDraftBasePath}/draft";
    public static readonly string ApiStarDraftPost = $"{ApiStarDraftBasePath}/draft";

    // Catalogs
    public static readonly string ApiCatalogsListGet = $"{ApiStarBasePath}/catalogs";
    public static readonly string ApiStarSingleCatalogsGet = $"{ApiStarBasePath}/catalogs";
    public static readonly string ApiCatalogPrimaryPost = $"{ApiStarBasePath}/starcatalog/primary";
    public static readonly string ApiCatalogPost = $"{ApiStarBasePath}/catalog";
    public static readonly string ApiStarStarCatalogDelete = $"api/star/starcatalog";
    public static readonly string ApiCatalogDelete = $"api/star/catalog";
    public static readonly string ApiStarStarCatalogPost = $"{ApiStarBasePath}/starcatalog";

    // Publications
    public static readonly string ApiStarPublicationPost = $"{ApiStarBasePath}/publication";

    // Observation Logs
    public static readonly string ApiStarObservationLogListGet = $"{ApiLightCurveBasePath}/logs";
    public static readonly string ApiStarObservationLogSingleGet = $"{ApiLightCurveBasePath}/logs";

    // Observatories
    public static readonly string ApiObservatoryBase = "api/observatory";
    public static readonly string ApiObservatoryListGet = $"{ApiObservatoryBase}";
    public static readonly string ApiObservatoryPost = $"{ApiObservatoryBase}";
    public static readonly string ApiObservatoryDelete = $"{ApiObservatoryBase}";
    public static readonly string ApiObservatoryEdit = $"{ApiObservatoryBase}";


    //      ---     Client     ---
    public static readonly string ClientStarBase = "/Stars";
    public static readonly string ClientStarListGet = $"{ClientStarBase}";
    public static readonly string ClientStarDraftListGet = $"{ClientStarBase}/Drafts";
    public static readonly string ClientStarDraftPost = $"{ClientStarBase}/Drafts/Create";
    public static readonly string ClientStarDraftSingle = $"{ClientStarBase}/Drafts";
    public static readonly string ClientStarSingleGet = $"{ClientStarBase}";

    public static readonly string ClientObservationLogBase = "/ObservationLogs";
    public static readonly string ClientObservationLogListGet = $"{ClientObservationLogBase}";
    public static readonly string ClientObservationLogSingleGet = $"{ClientObservationLogBase}";

    public static readonly string ClientObservatoriesBase = "/Observatories";
    public static readonly string ClientObservatoriesListGet = $"{ClientObservatoriesBase}";
    public static readonly string ClientObservatoriesPost = $"{ClientObservatoriesBase}/Add";

    public static readonly string ClientLightCurveBase = "/LightCurves";
    public static readonly string ClientLightCurveListGet = $"{ClientLightCurveBase}";
    public static readonly string ClientLightCurveSingleGet = $"{ClientLightCurveBase}";
    public static readonly string ClientLightCurvePost = $"{ClientLightCurveBase}/Add";

    public static readonly string ClientDeviceBase = "/Device";
    public static readonly string ClientDevicePost = $"{ClientDeviceBase}/Add";
    public static readonly string ClientDeviceListGet = $"{ClientDeviceBase}/List";

    public static readonly string ClientUserProfile = "/me";
    public static readonly string ClientAuthLogIn = "/Login";
    public static readonly string ClientAuthRegister = "/Register";
    public static readonly string ClientSearch = "/Search";
    public static readonly string ClientAdminBase = "/Admin";
    public static readonly string ClientAdminStars = $"{ClientAdminBase}/Stars";
    public static readonly string ClientAdminCatalogs = $"{ClientAdminBase}/Catalogs";
}