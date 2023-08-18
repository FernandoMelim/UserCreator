using AutoMapper;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.RepositoriesInterfaces;

namespace UserCreator.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

