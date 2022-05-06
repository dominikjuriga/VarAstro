using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class SearchQuery
{
    [Required(ErrorMessage = "Pole dotaz je povinné.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Dotaz musí mať dĺžku {2}-{1} znakov.")]
    public string Query { get; set; } = string.Empty;
}