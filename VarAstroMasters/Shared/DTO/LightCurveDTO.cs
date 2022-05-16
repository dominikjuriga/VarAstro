using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class LightCurveDTO
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public int StarId { get; set; }
    public StarBasicDTO Star { get; set; }
    public UserDTO User { get; set; }
    public DeviceDTO Device { get; set; }
    public ObservatoryDTO Observatory { get; set; }
    public string? DataFileLink { get; set; }
    public string? Values { get; set; }
    public PublishVariant PublishVariant { get; set; }
    [NotMapped] [JsonIgnore] public bool ValuesFinishedLoading { get; set; } = false;
    public string? Comment { get; set; }
    public List<Image> Images { get; set; }
}