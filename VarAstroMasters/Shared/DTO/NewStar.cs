using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using VarAstroMasters.Shared.Models;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.DTO;

public class NewStar
{
    [StringLength(100, MinimumLength = 5, ErrorMessage = $"Názov hviezdy: {Keywords.FormStringLength}")]
    [Required(ErrorMessage = $"Názov hviezdy: {Keywords.FormFieldRequired}")]
    public string? Name { get; set; }

    public string? UserIdentification { get; set; }

    [Required(ErrorMessage = $"Cross identifikácia: {Keywords.FormFieldRequired}")]
    [ValidateComplexType]
    public StarCatalog StarCatalog { get; set; } = new();

    [Required(ErrorMessage = $"Koordináty nového objektu: {Keywords.FormFieldRequired}")]
    [ValidateComplexType]

    public StarCoordDTO StarCoord { get; set; } = new();

    public double MAG { get; set; }
}