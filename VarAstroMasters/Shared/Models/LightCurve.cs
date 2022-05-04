using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VarAstroMasters.Shared.Models;

public class LightCurve
{
    public int Id { get; set; }
    public bool Publish { get; set; } = true;
    [Required] public string UserId { get; set; }
    public User User { get; set; }
    [Required] public int StarId { get; set; }

    public Star Star { get; set; }
    [Required] public string DataFileName { get; set; } = string.Empty;
    [Required] public string ImageFileName { get; set; } = string.Empty;
    [Required] public string Values { get; set; }
}