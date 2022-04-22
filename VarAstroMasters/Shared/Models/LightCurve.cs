using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VarAstroMasters.Shared.Models;

public class LightCurve
{
    public int Id { get; set; }
    public int Value { get; set; } = 42;
    [Required]
    public string UserId { get; set; }
    public User User { get; set; } 
    [Required]
    public int StarId { get; set; }

    public Star Star { get; set; }
}