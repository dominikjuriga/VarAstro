using System.Text.Json.Serialization;
using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class StarBasicDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
    public List<StarCatalog> StarCatalogs { get; set; }
}