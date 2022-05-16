using VarAstroMasters.Shared.Models;

namespace VarAstroMasters.Shared.DTO;

public class StarBasicDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
    public double RA { get; set; }
    public double DEC { get; set; }
    public List<StarCatalog> StarCatalogs { get; set; }
}