using AutoMapper;
using System.Net;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;
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

    public async Task<PostUserResponseDTO> CreateUser(PostUserRequestDTO postUserRequestDto)
    {
        var user = _mapper.Map<User>(postUserRequestDto);
        await _userRepository.CreateUser(user);

        return new PostUserResponseDTO()
        {
            Errors = null,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<DeleteUserResponseDTO> DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);

        return new DeleteUserResponseDTO()
        {
            Errors = null,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<PatchUserResponseDTO> EditUser(PatchUserRequestDTO patchUserRequestDto)
    {
        var user = _mapper.Map<User>(patchUserRequestDto);
        await _userRepository.EditUser(user);

        return new PatchUserResponseDTO()
        {
            Errors = null,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<GetAllUsersResponseDTO> GetAllUsers()
    {
        var allUsers = await _userRepository.GetAllUsers();

        return new GetAllUsersResponseDTO() { 
            Users = allUsers.ToList(),  
            Errors = null,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<GetUserResponseDTO> GetUserById(int id)
    {
        var user = await _userRepository.GetUser(id);

        return new GetUserResponseDTO()
        {
            User = user,
            Errors = null,
            StatusCode = HttpStatusCode.OK
        };
    }
}

