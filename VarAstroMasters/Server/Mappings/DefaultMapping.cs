﻿namespace VarAstroMasters.Server.Mappings;

using Shared.Static;

public class DefaultMapping : Profile
{
    public DefaultMapping()
    {
        CreateMap<Star, StarDTO>();
        CreateMap<Star, StarBasicDTO>();
        CreateMap<Device, DeviceDTO>();
        CreateMap<LightCurveAdd, LightCurve>();
        CreateMap<DeviceAdd, Device>();
        CreateMap<Observatory, ObservatoryDTO>();
        CreateMap<User, UserDTO>().ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        CreateMap<User, UserSimpleDTO>().ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<LightCurve, LightCurveDTO>();
        // .ForMember(dest => dest.DataFileLink,
        //     opt => opt.MapFrom(src => $"{Endpoints.ApiLightCurveBasePath}/{src.Id}/file"));
    }
}