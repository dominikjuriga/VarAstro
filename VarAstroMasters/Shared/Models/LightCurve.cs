using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VarAstroMasters.Shared.Models;

public class LightCurve
{
    public int Id { get; set; }
    public PublishVariant PublishVariant { get; set; } = PublishVariant.None;
    [Required] public string UserId { get; set; }
    public User User { get; set; }
    [Required] public int StarId { get; set; }

    public Star Star { get; set; }
    [Required] public string DataFileContent { get; set; } = string.Empty;
    [Required] public string ImageFileName { get; set; } = string.Empty;
    public int? DeviceId { get; set; }
    public Device? Device { get; set; }
    public int? ObservatoryId { get; set; }
    public Observatory? Observatory { get; set; }
}