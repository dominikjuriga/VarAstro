using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class ObservationLogDetailDTO
{
    public List<LightCurveDTO> Curves { get; set; }
    public UserDTO User { get; set; }
}