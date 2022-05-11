using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class Observatory
{
    public int Id { get; set; }

    [Range(-180, 180, ErrorMessage = Keywords.FormValueRange)]
    public double Longitude { get; set; } = 16.606836d;


    [Range(-90, 90, ErrorMessage = Keywords.FormValueRange)]
    public double Latitude { get; set; } = 49.195061d;

    public string Address { get; set; }
    public string UserId { get; set; } = string.Empty;
    [NotMapped] public bool New { get; set; } = true;
}