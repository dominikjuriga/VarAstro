using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.DTO;

public class NewLightCurve
{
    [Required] [ValidateComplexType] public NewStar NewStarObj { get; set; } = new();
    public StarDTO? ExistingStar { get; set; }
    public UserStarIdentificationCreateDTO UserIdentification { get; set; } = new();
    [Required] [ValidateComplexType] public LightCurveAdd LightCurve { get; set; } = new();
    public bool CreateStarOnSubmit { get; set; } = false;
}