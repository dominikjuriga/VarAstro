using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class Star
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<LightCurve> LightCurves { get; set; } = new();
    public List<StarCatalog> StarCatalogs { get; set; }
    public StarVariability StarVariability { get; set; }
    public StarPublish StarPublish { get; set; }
}