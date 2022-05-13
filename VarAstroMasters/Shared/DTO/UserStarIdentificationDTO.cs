using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VarAstroMasters.Shared.DTO;

public class UserStarIdentificationDTO
{
    public StarBasicDTO Star { get; set; }
    public string UserIdentification { get; set; } = string.Empty;
}