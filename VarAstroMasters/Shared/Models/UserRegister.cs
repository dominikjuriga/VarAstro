using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class UserRegister
{
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [EmailAddress(ErrorMessage = Keywords.FormInvalidFormat)]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [StringLength(100, MinimumLength = 2, ErrorMessage = Keywords.FormStringLength)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [StringLength(100, MinimumLength = 2, ErrorMessage = Keywords.FormStringLength)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = Keywords.FormStringLength, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [DataType(DataType.Password, ErrorMessage = Keywords.FormInvalidFormat)]
    [Compare("Password", ErrorMessage = Keywords.FormPasswordMismatch)]
    public string ConfirmPassword { get; set; } = string.Empty;
}