using AutoMapper;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Infrastructure.DtosEntitiesMappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<PostUserRequestDTO, User>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.Name}")
            )
            .ForMember(
                dest => dest.Phone,
                opt => opt.MapFrom(src => $"{src.Phone}")
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => $"{src.Email}")
            )
            .ForMember(
                dest => dest.BirthDate,
                opt => opt.MapFrom(src => $"{src.BirthDate}")
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => $"{src.Email}")
            );

        CreateMap<PatchUserRequestDTO, User>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.Name}")
            )
            .ForMember(
                dest => dest.Phone,
                opt => opt.MapFrom(src => $"{src.Phone}")
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => $"{src.Email}")
            )
            .ForMember(
                dest => dest.BirthDate,
                opt => opt.MapFrom(src => $"{src.BirthDate}")
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => $"{src.Email}")
            )
            .ForMember(
            dest => dest.Adresses,
            opt => opt.MapFrom(src => src.Adresses));
    }
}

