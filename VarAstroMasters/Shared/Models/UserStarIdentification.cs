using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.DTO;

namespace VarAstroMasters.Shared.Models;

public class UserStarIdentification
{
    [Required] [Key] public int StarId { get; set; }
    [Required] [Key] public string UserId { get; set; } = string.Empty;
    public Star Star { get; set; }
    public string Identification { get; set; } = string.Empty;
}