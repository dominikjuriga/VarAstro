using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Models;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.DTO;

public class NewLightCurve
{
    public NewStar NewStar { get; set; } = new();
    public StarDTO? ExistingStar { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public List<Image> Images { get; set; } = new();

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string JD { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string PhotometricSystem { get; set; } = string.Empty;

    public double VarAperture { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string Filter { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public PublishVariant PublishVariant { get; set; } = PublishVariant.All;

    public int DeviceId { get; set; } = 0;
    public int ObservatoryId { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public bool CreateStarOnSubmit { get; set; } = false;
}