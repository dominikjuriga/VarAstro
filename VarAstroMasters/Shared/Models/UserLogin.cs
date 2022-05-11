using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class UserLogin
{
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [EmailAddress(ErrorMessage = Keywords.FormInvalidFormat)]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [DataType(DataType.Password, ErrorMessage = Keywords.FormInvalidFormat)]
    public string Password { get; set; } = string.Empty;
}