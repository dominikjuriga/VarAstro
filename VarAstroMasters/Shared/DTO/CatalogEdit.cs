using System.ComponentModel.DataAnnotations.Schema;

namespace VarAstroMasters.Shared.Models;

public class CatalogEdit
{
    public string Name { get; set; }

    [NotMapped] public string? OriginalName { get; set; }

    [NotMapped] public bool New { get; set; } = true;
}