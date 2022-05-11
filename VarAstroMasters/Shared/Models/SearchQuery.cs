using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class SearchQuery
{
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = Keywords.FormStringLength)]
    public string Query { get; set; } = string.Empty;
}