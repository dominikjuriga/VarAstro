using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class ObservatoryDTO
{
    public int Id { get; set; }

    [Column(TypeName = "decimal(10,8)")]
    [Range(-180, 180)]
    public decimal Longitude { get; set; } = 16.606836m;

    [Column(TypeName = "decimal(10,8)")]
    [Range(-90, 90)]
    public decimal Latitude { get; set; } = 49.195061m;

    public string Address { get; set; }
}