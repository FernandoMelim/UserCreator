using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;

namespace UserCreator.Application.ServicesInterfaces;

public interface IUserService
{
    Task<PostUserResponseDTO> CreateUser(PostUserRequestDTO postUserRequestDto);
    Task<DeleteUserResponseDTO> DeleteUser(int id);
    Task<PatchUserResponseDTO> EditUser(PatchUserRequestDTO patchUserRequestDto);
    Task<GetAllUsersResponseDTO> GetAllUsers();
    Task<GetUserResponseDTO> GetUserById(int id);
}

