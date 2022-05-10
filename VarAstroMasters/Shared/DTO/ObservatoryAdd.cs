using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.DTO;

public class ObservatoryAdd
{
    [Column(TypeName = "decimal(10,8)")]
    [Required(ErrorMessage = "Pole zemepisná dĺžka je povinné.")]
    [Range(-180, 180, ErrorMessage = "Hodnota je mimo rozsahu {2}-{1}")]
    public decimal Longitude { get; set; } = 16.606836m;

    [Column(TypeName = "decimal(10,8)")]
    [Required(ErrorMessage = "Pole zemepisná šírka je povinné.")]
    [Range(-90, 90, ErrorMessage = "Hodnota je mimo rozsahu {2}-{1}")]
    public decimal Latitude { get; set; } = 49.195061m;

    [Required(ErrorMessage = "Pole adresa je povinné.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Adresa musí mať {2} až {1} znakov.")]
    public string Address { get; set; } = string.Empty;
}