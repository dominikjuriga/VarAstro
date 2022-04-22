using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class UserRegister
{
    [Required]
    [EmailAddress]
    [Display(Name ="Email Address")]
    public string EmailAddress { get; set;} = String.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Your password must be between {2} and {1} characters", MinimumLength = 8)]
    [Display(Name ="Password")]
    public string Password { get; set;} = String.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name ="Confirm Password")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set;} = String.Empty;
}