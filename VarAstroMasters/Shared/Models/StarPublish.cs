using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.DTO;

namespace VarAstroMasters.Shared.Models;

public class StarPublish
{
    public int Id { get; set; }
    public int StarId { get; set; } = -1;
    [NotMapped] public string? StarName { get; set; }
    public int Year { get; set; }
    public string Discoverer { get; set; }
    public string PublicationLink { get; set; }
}