using AutoMapper;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Infrastructure.DtosEntitiesMappers;

public class UserMapper : Profile
{
    public UserMapper()
    {

        CreateMap<PostUserRequestDTO, User>();
        CreateMap<PatchUserRequestDTO, User>();
        CreateMap<User, UserResponseDTO>();
    }
}

