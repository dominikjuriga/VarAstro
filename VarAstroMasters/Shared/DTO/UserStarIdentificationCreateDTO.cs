namespace VarAstroMasters.Shared.DTO;

public class UserStarIdentificationCreateDTO
{
    public int StarId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserIdentification { get; set; } = string.Empty;
}