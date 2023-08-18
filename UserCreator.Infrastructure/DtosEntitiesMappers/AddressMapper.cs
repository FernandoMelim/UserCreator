using AutoMapper;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Infrastructure.DtosEntitiesMappers;

public class AddressMapper : Profile
{
    public AddressMapper()
    {
        CreateMap<CreateAddressRequestDTO, Address>();
        CreateMap<ChangeAddressRequestDTO, Address>();
        CreateMap<Address, AddressResponseDTO>();
    }
}

