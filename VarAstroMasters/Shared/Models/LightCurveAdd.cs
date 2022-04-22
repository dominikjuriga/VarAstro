using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class LightCurveAdd
{
    [Required] public string Name { get; set; } = String.Empty;
    [Required] public int StarId { get; set; }
    [Required] public int Value { get; set; }
}