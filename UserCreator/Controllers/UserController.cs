using Microsoft.AspNetCore.Mvc;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;

namespace UserCreator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet("{id}")]
    public async Task<GetUserResponseDTO> Get([FromRoute] int id)
    {
        var result = await _userService.GetUserById(id);
        return result;
    }

    [HttpGet]
    public async Task<GetAllUsersResponseDTO> Get()
    {
        var result = await _userService.GetAllUsers();
        return result;
    }

    [HttpPost]
    public async Task<PostUserResponseDTO> Post([FromBody] PostUserRequestDTO postUserRequestDto)
    {
        var result = await _userService.CreateUser(postUserRequestDto);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<DeleteUserResponseDTO> Delete([FromRoute] int id)
    {
        var result = await _userService.DeleteUser(id);
        return result;
    }

    [HttpPatch]
    public async Task<PatchUserResponseDTO> Patch([FromBody] PatchUserRequestDTO patchUserRequestDto)
    {
        var result = await _userService.EditUser(patchUserRequestDto);
        return result;
    }
}

