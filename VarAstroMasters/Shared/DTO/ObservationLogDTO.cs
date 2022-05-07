namespace VarAstroMasters.Shared.DTO;

public class ObservationLogDTO
{
    public UserSimpleDTO User { get; set; }
    public int Contributions { get; set; }
    public DateTime? LastContribution { get; set; }
}