using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class LightCurve
{
    public int Id { get; set; }
    public PublishVariant PublishVariant { get; set; } = PublishVariant.None;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string UserId { get; set; }

    public User User { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public int StarId { get; set; }

    public Star Star { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string DataFileContent { get; set; } = string.Empty;

    public List<Image> Images { get; set; } = new();
    public int? DeviceId { get; set; }
    public Device? Device { get; set; }
    public string? Comment { get; set; }
    public int? ObservatoryId { get; set; }
    public Observatory? Observatory { get; set; }
}