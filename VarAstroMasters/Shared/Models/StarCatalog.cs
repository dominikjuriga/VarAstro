using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class StarCatalog
{
    [Key]
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public int StarId { get; set; }

    [Key]
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string CatalogId { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double Ra { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double Dec { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double Mag { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string CrossId { get; set; }

    public bool Primary { get; set; } = false;
    [NotMapped] public bool New { get; set; } = true;
}