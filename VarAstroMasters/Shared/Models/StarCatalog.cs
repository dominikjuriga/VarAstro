using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class StarCatalog
{
    [Required] public int StarId { get; set; }

    [Required] public string CatalogId { get; set; }

    [Required] public string Ra { get; set; }

    [Required] public string Dec { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    [Required]
    public decimal Mag { get; set; }

    [Required] public string CrossId { get; set; }
    public bool Primary { get; set; } = false;
}