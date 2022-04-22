using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class Star
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "decimal(10, 3)")]
    public decimal RA { get; set; }

    public List<LightCurve> LightCurves { get; set; } = new List<LightCurve>();
}