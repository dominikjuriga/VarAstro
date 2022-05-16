using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

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

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double Epoch { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double Period { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public double PrimaryMinimum { get; set; }
}