using AutoMapper;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Infrastructure.DtosEntitiesMappers;

public class AddressMapper : Profile
{
    public AddressMapper()
    {
        CreateMap<CreateAddressRequestDTO, Address>()
            .ForMember(
                dest => dest.Street,
                opt => opt.MapFrom(src => $"{src.Street}")
            )
            .ForMember(
                dest => dest.Number,
                opt => opt.MapFrom(src => src.Number)
            )
            .ForMember(
                dest => dest.City,
                opt => opt.MapFrom(src => $"{src.City}")
            )
            .ForMember(
                dest => dest.State,
                opt => opt.MapFrom(src => $"{src.State}")
            )
            .ForMember(
                dest => dest.PostalCode,
                opt => opt.MapFrom(src => $"{src.PostalCode}")
            );

        CreateMap<ChangeAddressRequestDTO, Address>()
             .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Street,
                opt => opt.MapFrom(src => $"{src.Street}")
            )
            .ForMember(
                dest => dest.Number,
                opt => opt.MapFrom(src => src.Number)
            )
            .ForMember(
                dest => dest.City,
                opt => opt.MapFrom(src => $"{src.City}")
            )
            .ForMember(
                dest => dest.State,
                opt => opt.MapFrom(src => $"{src.State}")
            )
            .ForMember(
                dest => dest.PostalCode,
                opt => opt.MapFrom(src => $"{src.PostalCode}")
            );
    }
}

