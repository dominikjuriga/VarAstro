using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Required] public string UserId { get; set; }
    public User User { get; set; }
}