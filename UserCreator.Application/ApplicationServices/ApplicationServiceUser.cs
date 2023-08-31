using AutoMapper;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Services;

namespace UserCreator.Application.ApplicationServices;

public class ApplicationServiceUser : IApplicationServiceUser
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public ApplicationServiceUser(IMapper mapper, IUserService userService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task CreateUser(PostUserRequestDTO postUserRequestDto)
    {
        var user = _mapper.Map<User>(postUserRequestDto);
        await _userService.CreateUser(user);
    }

    public async Task DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
    }

    public async Task EditUser(PatchUserRequestDTO patchUserRequestDto)
    {
        var user = _mapper.Map<User>(patchUserRequestDto);
        await _userService.EditUser(user);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var allUsers = await _userService.GetAllUsers();
        return allUsers;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        return user;
    }
}

