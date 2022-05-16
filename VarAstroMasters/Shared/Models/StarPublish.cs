using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.Models;

public class StarPublish
{
    public int Id { get; set; }
    public int StarId { get; set; } = -1;
    [NotMapped] public string? StarName { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public int Year { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string Discoverer { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    public string PublicationLink { get; set; }
}