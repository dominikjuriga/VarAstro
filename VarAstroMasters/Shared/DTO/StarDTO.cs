using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class StarDTO
{
    public int Id { get; set; }
    public double RA { get; set; }
    public double DEC { get; set; }
    public string Name { get; set; }
    public List<UserStarIdentificationDTO>? Identification { get; set; }
    public List<LightCurveDTO>? LightCurves { get; set; }
    public List<StarCatalog>? StarCatalogs { get; set; }
    public StarVariability? StarVariability { get; set; }
    public StarPublish? StarPublish { get; set; }
}