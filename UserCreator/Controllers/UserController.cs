using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.Validations;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses;
using UserCreator.Domain.DTOs.Responses.User;

namespace UserCreator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,
        IValidationNotifications validationNotifications,
        IMapper mapper)
        : base(validationNotifications)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(GetUserResponseDTO))]
    [ProducesResponseType(404, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<GetUserResponseDTO>> Get([FromRoute] int id)
    {
        var user = await _userService.GetUserById(id);

        var getUserResponseDTO = new GetUserResponseDTO
        {
            User = _mapper.Map<UserResponseDTO>(user)
        };

        return await Return(getUserResponseDTO);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(GetAllUsersResponseDTO))]
    public async Task<ActionResult<GetAllUsersResponseDTO>> Get()
    {
        var result = await _userService.GetAllUsers();

        var dto =  new GetAllUsersResponseDTO()
        {
            Users = _mapper.Map<List<UserResponseDTO>>(result),
        };

        return await Return(dto);
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PostUserResponseDTO))]
    [ProducesResponseType(422, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<PostUserResponseDTO>> Post([FromBody] PostUserRequestDTO postUserRequestDto)
    {
        await _userService.CreateUser(postUserRequestDto);
        return await Return(new PostUserResponseDTO());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(200, Type = typeof(DeleteUserResponseDTO))]
    [ProducesResponseType(404, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<DeleteUserResponseDTO>> Delete([FromRoute] int id)
    {
        await _userService.DeleteUser(id);
        return await Return(new DeleteUserResponseDTO());
    }

    [HttpPatch]
    [ProducesResponseType(200, Type = typeof(PatchUserResponseDTO))]
    [ProducesResponseType(404, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(422, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<PatchUserResponseDTO>> Patch([FromBody] PatchUserRequestDTO patchUserRequestDto)
    {
        await _userService.EditUser(patchUserRequestDto);
        return await Return(new PatchUserResponseDTO());
    }
}

