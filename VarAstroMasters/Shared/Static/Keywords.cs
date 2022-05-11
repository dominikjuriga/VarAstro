namespace VarAstroMasters.Shared.Static;

public static class Keywords
{
    public static readonly string CORS_Policy = "CORS_Policy";
    public static readonly string JWT_Issuer = "Jwt:Issuer";
    public static readonly string JWT_Key = "Jwt:Key";
    public static readonly string JWT_Bearer_Token = "bearerToken";
    public static readonly string AuthType = "jwt";
    public static readonly string Role_User = "User";
    public static readonly string Role_Admin = "Administrator";
    public static readonly string Client_HTTP_Bearer_Header = "bearer";
    public static readonly string LcJdFormatHeliocentric = "heliocentric";
    public static readonly string LcJdFormatGeocentric = "geocentric";
    public static readonly string LcFilterNoFilter = "NoFilter";
    public static readonly string LcFilterU = "U";
    public static readonly string LcPhotometricInstrumental = "instrumental";
    public static readonly string LcPhotometricStandard = "standard";
    public static readonly string DefaultPageTitle = "Var Astro";
    public static readonly string DefaultDateTimeFormat = "dd.MM.yyyy HH:mm";
    public static readonly string AlertSuccess = "success";
    public static readonly string AlertInfo = "info";
    public static readonly string AlertDanger = "danger";
    public static readonly string AcceptedImageFormats = "image/png, image/jpeg, image/gif";
    public static readonly string NotFoundMessage = "Objekt nenájdený.";
    public static readonly string AlreadyExists = "Objekt so zhodným identifikátorom už existuje.";
    public static readonly string NotPublished = "Tento záznam nie je verejný.";
    public static readonly string CurveNotPublished = "Toto pozorovanie nemá zverejnenú krivku.";
    public static readonly string MapNotPublished = "Toto pozorovanie nemá zverejnenú mapu.";
    public static readonly string RegisterFailed = "Registrácia zlyhala.";
    public static readonly string RegisterSucceeded = "Registrácia úspešná.";
    public static readonly string InvalidCredentials = "Nesprávne údaje.";
    public static readonly string InvalidToken = "Vaša požiadavka neobsahuje autentifikačný token. Ste prihlásený?";
    public static readonly string PostSucceeded = "Záznam bol vytvorený.";
    public static readonly string DeleteSucceeded = "Záznam bol odstránený.";
    public static readonly string PutSucceeded = "Záznam bol upravený.";
    public static readonly string PutFailed = "Úprava záznamu bola neúspešná.";
    public static readonly string PostFailed = "Vytvorenie záznamu bolo neúspešné.";
    public static readonly string DeleteFailed = "Odstránenie záznamu bolo neúspešné.";
    public static readonly string CannotDeletePrimaryCat = "Primárny katalóg nie je možné odstrániť.";
    public static readonly string SearchSucceeded = "Pre zadaný výraz bolo nájdených";
    public static readonly string SearchFailed = "Pre zadaný výraz neboli nájdené žiadne výsledky.";
    public static readonly int DefaultFilter = 0;
    public const string FormStringLength = "Pole musí obsahovať {2} až {1} znakov.";
    public const string FormValueRange = "Hodnota pola musí byť v rozmedzí {2} až {1}.";
    public const string FormFieldRequired = "Toto pole je povinné.";
    public const string FormInvalidFormat = "Táto hodnota nie je pre toto pole platná.";
    public const string FormPasswordMismatch = "Heslá sa musia zhodovať.";
}