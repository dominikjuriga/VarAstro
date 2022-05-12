using VarAstroMasters.Shared.DTO;

namespace VarAstroMasters.Shared.Helpers;

public static class StarCoordsHelper
{
    public static double? RaToRadians(StarCoordDTO coords)
    {
        if (coords is { RaH: not null, RaM: not null, RaS: not null })
            return coords.RaH * 15 + coords.RaM / 4d + coords.RaS / 240d;
        return null;
    }

    public static double? DecToRadians(StarCoordDTO coords)
    {
        if (coords is { DecD: not null, DecM: not null, DecS: not null })
        {
            if (coords.DecD >= 0)
                return coords.DecD + coords.DecM / 60d + coords.DecS / 3600d;
            else
                return coords.DecD - coords.DecM / 60d - coords.DecS / 3600d;
        }

        return null;
    }
}