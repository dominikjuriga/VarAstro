using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using VarAstroMasters.Shared.Models;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.DTO;

public class NewStar
{
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [StringLength(100, MinimumLength = 5, ErrorMessage = Keywords.FormStringLength)]
    public string Name { get; set; }

    public string? UserIdentification { get; set; }

    public StarCatalog StarCatalog { get; set; } = new();

    public StarCoordDTO StarCoord { get; set; } = new();
    public double MAG { get; set; }
}