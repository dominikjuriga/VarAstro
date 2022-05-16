﻿namespace VarAstroMasters.Shared.DTO;

public class ObservationLogDetailDTO
{
    public List<LightCurveDTO> Curves { get; set; }
    public List<StarBasicDTO> DistinctStars { get; set; }
    public UserDTO User { get; set; }
}