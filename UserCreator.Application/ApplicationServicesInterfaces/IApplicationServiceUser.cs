using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Application.ApplicationServicesInterfaces;

public interface IApplicationServiceUser
{
    Task<PostUserResponseDTO> CreateUser(PostUserRequestDTO postUserRequestDto);
    Task<DeleteUserResponseDTO> DeleteUser(int id);
    Task<PatchUserResponseDTO> EditUser(PatchUserRequestDTO patchUserRequestDto);
    Task<GetAllUsersResponseDTO> GetAllUsers();
    Task<GetUserResponseDTO> GetUserById(int id);
}

