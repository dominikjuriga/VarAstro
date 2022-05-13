using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.DTO;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class Star
{
    public int Id { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string Name { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double RA { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double DEC { get; set; }

    public List<UserStarIdentification>? Identification { get; set; }

    public List<LightCurve> LightCurves { get; set; } = new();
    public List<StarCatalog> StarCatalogs { get; set; }
    public StarVariability StarVariability { get; set; }
    public StarPublish StarPublish { get; set; }
}