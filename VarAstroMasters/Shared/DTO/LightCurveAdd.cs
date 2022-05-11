using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Models;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.DTO;

public class LightCurveAdd
{
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public int StarId { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string DataFileContent { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]

    public string? JD { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]

    public string? Filter { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]

    public string? PhotometricSystem { get; set; } = null;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]

    public PublishVariant PublishVariant { get; set; } = PublishVariant.All;

    public string Comment { get; set; } = string.Empty;
    public int DeviceId { get; set; } = 0;
    public List<Image> Images { get; set; } = new();
}