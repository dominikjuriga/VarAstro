using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class StarCatalog
{
    [Key]
    [Required(ErrorMessage = $"ID Hviezdy: {Keywords.FormFieldRequired}")]
    public int StarId { get; set; }

    [Key]
    [Required(ErrorMessage = $"ID katalógu: {Keywords.FormFieldRequired}")]
    public string CatalogId { get; set; }

    [Required(ErrorMessage = $"Ra Hviezdy: {Keywords.FormFieldRequired}")]
    public double Ra { get; set; }

    [Required(ErrorMessage = $"Dec: {Keywords.FormFieldRequired}")]
    public double Dec { get; set; }

    [Required(ErrorMessage = $"Mag: {Keywords.FormFieldRequired}")]
    public double Mag { get; set; }

    [Required(ErrorMessage = $"Cross ID: {Keywords.FormFieldRequired}")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = $"Cross ID {Keywords.FormStringLength}")]
    public string CrossId { get; set; }

    public bool Primary { get; set; } = false;
    [NotMapped] public bool New { get; set; } = true;
}