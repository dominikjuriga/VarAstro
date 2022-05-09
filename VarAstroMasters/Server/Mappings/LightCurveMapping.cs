namespace VarAstroMasters.Server.Mappings;

public class LightCurveMapping : Profile
{
    public LightCurveMapping()
    {
        CreateMap<Star, StarDTO>();
        CreateMap<Star, StarBasicDTO>();
        CreateMap<Device, DeviceDTO>();
        CreateMap<LightCurveAdd, LightCurve>();
        CreateMap<Observatory, ObservatoryDTO>();
        CreateMap<User, UserDTO>().ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        CreateMap<LightCurve, LightCurveDTO>();
    }
}