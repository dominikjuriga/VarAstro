using System.ComponentModel.DataAnnotations;
using VarAstroMasters.Shared.Static;

namespace VarAstroMasters.Shared.DTO;

public class StarCoordDTO
{
    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [Range(0, 23, ErrorMessage = Keywords.FormValueRange)]
    public int? RaH { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [Range(0, 59, ErrorMessage = Keywords.FormValueRange)]
    public int? RaM { get; set; }


    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [Range(0, 59, ErrorMessage = Keywords.FormValueRange)]
    public int? RaS { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [Range(-90, 90, ErrorMessage = Keywords.FormValueRange)]
    public int? DecD { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [Range(0, 59, ErrorMessage = Keywords.FormValueRange)]
    public int? DecM { get; set; }

    [Required(ErrorMessage = Keywords.FormFieldRequired)]
    [Range(0, 59, ErrorMessage = Keywords.FormValueRange)]
    public int? DecS { get; set; }
}