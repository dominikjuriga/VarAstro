using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class StarCatalog
{
    [Key] [Required] public int StarId { get; set; }

    [Key]
    [Required(ErrorMessage = "Pole Katalóg je povinné.")]
    public string CatalogId { get; set; }

    [Required(ErrorMessage = "Pole Ra je povinné.")]
    public string Ra { get; set; }

    [Required(ErrorMessage = "Pole Dec je povinné.")]
    public string Dec { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    [Required(ErrorMessage = "Pole Mag je povinné.")]
    public decimal Mag { get; set; }

    [Required(ErrorMessage = "Pole Cross ID je povinné.")]
    public string CrossId { get; set; }

    public bool Primary { get; set; } = false;
    [NotMapped] public bool New { get; set; } = true;
}