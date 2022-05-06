using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public enum VariabilityType
{
    Intrinsic, // luminosity actually changes
    Extrinsic // change in luminosity is caused by other objects interfering with the light on the way
}

public class StarVariability
{
    public int Id { get; set; }
    public int StarId { get; set; }
    public VariabilityType VariabilityType { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 9)")]

    public decimal Epoch { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 9)")]
    public decimal Period { get; set; }

    [Required] public double PrimaryMinimum { get; set; }
}