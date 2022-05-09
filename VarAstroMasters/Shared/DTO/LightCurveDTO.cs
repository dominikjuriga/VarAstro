using Microsoft.AspNetCore.Identity;
using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class LightCurveDTO
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public StarDTO Star { get; set; }
    public UserDTO User { get; set; }
    public DeviceDTO? Device { get; set; }
    public ObservatoryDTO Observatory { get; set; }
    public string? DataFileLink { get; set; }
    public string? Values { get; set; }
    public string? Comment { get; set; }
    public List<Image> Images { get; set; }
}