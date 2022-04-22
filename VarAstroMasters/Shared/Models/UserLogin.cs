using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class UserLogin
{
    [Required]
    [EmailAddress]
    [Display(Name ="Email Address")]
    public string EmailAddress { get; set;} = String.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name ="Password")]
    public string Password { get; set;} = String.Empty;
}