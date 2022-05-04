using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class LightCurveAdd
{
    [Required] public int StarId { get; set; }
    [Required] public IFormFile DataFile { get; set; }

    [Required(ErrorMessage = "Pole Formát JD je povinné.")]
    public string? JD { get; set; } = null;


    [Required(ErrorMessage = "Pole filter je povinné.")]
    public string? Filter { get; set; } = null;

    [Required(ErrorMessage = "Pole fotometrický systém je povinné.")]
    public string? PhotometricSystem { get; set; } = null;

    [Required(ErrorMessage = "Pole zverejniť pozorovanie je povinné.")]
    public PublishVariant PublishVariant { get; set; } = PublishVariant.All;

    public string Comment { get; set; } = string.Empty;
}