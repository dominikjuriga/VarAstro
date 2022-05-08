using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class Catalog
{
    [Key] public string Name { get; set; }
    [NotMapped] public bool New { get; set; } = true;
}