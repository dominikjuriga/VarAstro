using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class UserRegister
{
    [Required(ErrorMessage = "Pole Email je povinné.")]
    [EmailAddress(ErrorMessage = "Pole musí obsahovať email vo formáte [identifikátor]@[doména].[TLD]")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pole Meno je povinné.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Meno musí mať dĺžku {2}-{1} znakov.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pole Priezvisko je povinné.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Priezvisko musí mať dĺžku {2}-{1} znakov.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pole Heslo je povinné.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Heslo musí mať dĺžku {2}-{1} znakov", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pole Potvrdenie hesla je povinné.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Heslá sa nezhodujú.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}