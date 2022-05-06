using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class Catalog
{
    [Key] public string Name { get; set; }
}