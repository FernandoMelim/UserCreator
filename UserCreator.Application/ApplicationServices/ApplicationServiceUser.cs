using AutoMapper;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Domain.DTOs.Responses.User;
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

    public async Task<PostUserResponseDTO> CreateUser(PostUserRequestDTO postUserRequestDto)
    {
        var user = _mapper.Map<User>(postUserRequestDto);
        await _userService.CreateUser(user);
        return new PostUserResponseDTO();
    }

    public async Task<DeleteUserResponseDTO> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return new DeleteUserResponseDTO();
    }

    public async Task<PatchUserResponseDTO> EditUser(PatchUserRequestDTO patchUserRequestDto)
    {
        var user = _mapper.Map<User>(patchUserRequestDto);
        await _userService.EditUser(user);
        return new PatchUserResponseDTO();
    }

    public async Task<GetAllUsersResponseDTO> GetAllUsers()
    {
        var allUsers = await _userService.GetAllUsers();

        var dto = new GetAllUsersResponseDTO()
        {
            Users = _mapper.Map<List<UserResponseDTO>>(allUsers)
        };

        return dto;
    }

    public async Task<GetUserResponseDTO> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);

        var getUserResponseDTO = new GetUserResponseDTO
        {
            User = _mapper.Map<UserResponseDTO>(user)
        };

        return getUserResponseDTO;
    }
}

