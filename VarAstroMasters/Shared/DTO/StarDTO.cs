using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class StarDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<LightCurveDTO> LightCurves { get; set; }
    public List<StarCatalog> StarCatalogs { get; set; }
}