using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Models;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.DTO;

public class LightCurveAdd
{
    [Required(ErrorMessage = $"StarID: {Keywords.FormFieldRequired}")]
    public int StarId { get; set; }

    [Required(ErrorMessage = $"Obsah súboru: {Keywords.FormFieldRequired}")]
    public string DataFileContent { get; set; }

    [Required(ErrorMessage = $"JD: {Keywords.FormFieldRequired}")]
    public string? JD { get; set; }

    [Required(ErrorMessage = $"Filter: {Keywords.FormFieldRequired}")]
    public string? Filter { get; set; }

    [Required(ErrorMessage = $"Fotometrický systém: {Keywords.FormFieldRequired}")]
    public string? PhotometricSystem { get; set; } = null;

    public double? VarAperture { get; set; }

    [Required(ErrorMessage = $"Zverejnenie: {Keywords.FormFieldRequired}")]

    public PublishVariant PublishVariant { get; set; } = PublishVariant.All;

    public string Comment { get; set; } = string.Empty;
    public int DeviceId { get; set; } = 0;
    public int ObservatoryId { get; set; } = 0;

    [Required(ErrorMessage = $"Mapka: {Keywords.FormFieldRequired}")]
    [MinLength(1, ErrorMessage = $"Mapka: {Keywords.FormFieldRequired}")]
    [ValidateComplexType]
    public List<Image> Images { get; set; } = new();
}