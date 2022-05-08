using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class DeviceAdd
{
    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Názov zariadenia musí mať dĺžku {2}-{1} znakov")]
    public string Name { get; set; } = string.Empty;
}