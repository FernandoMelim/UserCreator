using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.RepositoriesInterfaces;

namespace UserCreator.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository; 

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public Task<PostUserResponseDTO> CreateUser(PostUserRequestDTO postUserRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteUserResponseDTO> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PatchUserResponseDTO> EditUser(PatchUserRequestDTO patchUserRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetAllUsersResponseDTO> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<GetUserResponseDTO> GetUserById(int id)
    {
        throw new NotImplementedException();
    }
}

