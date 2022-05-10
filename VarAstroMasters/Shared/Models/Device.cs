using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class Device
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = Keywords.StringLength)]
    public string Name { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public User? User { get; set; }
    [NotMapped] public bool New { get; set; } = true;
}