using Microsoft.AspNetCore.Identity;
using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class LightCurveDTO
{
    public int Id { get; set; }
    public int Value { get; set; }
    public StarDTO Star { get; set; }
    public UserDTO User { get; set; }
    public DeviceDTO? Device { get; set; }
    public ObservatoryDTO Observatory { get; set; }
}