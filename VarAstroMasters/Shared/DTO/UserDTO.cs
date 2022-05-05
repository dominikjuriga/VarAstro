using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<DeviceDTO> Devices { get; set; }
    public List<ObservatoryDTO> Observatories { get; set; }
}