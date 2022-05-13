using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class Image
{
    public int Id { get; set; }

    [Required(ErrorMessage = $"Obrázok: {Keywords.FormFieldRequired}")]
    public string Data { get; set; } = string.Empty;
}