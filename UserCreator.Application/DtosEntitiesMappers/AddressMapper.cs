using AutoMapper;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Application.DtosEntitiesMappers;

public class AddressMapper : Profile
{
    public AddressMapper()
    {
        CreateMap<CreateAddressRequestDTO, Address>();
        CreateMap<ChangeAddressRequestDTO, Address>();
        CreateMap<Address, AddressResponseDTO>();
    }
}

