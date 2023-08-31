using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Interfaces.Services;
using UserCreator.Domain.Validations;

namespace UserCreator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ApiControllerBase
{
    private readonly IApplicationServiceUser _applicationServiceUser;
    private readonly IMapper _mapper;

    public UserController(IApplicationServiceUser applicationServiceUser,
        IValidationNotifications validationNotifications,
        IMapper mapper)
        : base(validationNotifications)
    {
        _applicationServiceUser = applicationServiceUser ?? throw new ArgumentNullException(nameof(applicationServiceUser));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(GetUserResponseDTO))]
    [ProducesResponseType(404, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<GetUserResponseDTO>> Get([FromRoute] int id)
    {
        var user = await _applicationServiceUser.GetUserById(id);
        return await Return(user);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(GetAllUsersResponseDTO))]
    public async Task<ActionResult<GetAllUsersResponseDTO>> Get()
    {
        var result = await _applicationServiceUser.GetAllUsers();
        return await Return(result);
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(PostUserResponseDTO))]
    [ProducesResponseType(422, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<PostUserResponseDTO>> Post([FromBody] PostUserRequestDTO postUserRequestDto)
    {
        var result = await _applicationServiceUser.CreateUser(postUserRequestDto);
        return await Return(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(200, Type = typeof(DeleteUserResponseDTO))]
    [ProducesResponseType(404, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<DeleteUserResponseDTO>> Delete([FromRoute] int id)
    {
        var result = await _applicationServiceUser.DeleteUser(id);
        return await Return(result);
    }

    [HttpPatch]
    [ProducesResponseType(200, Type = typeof(PatchUserResponseDTO))]
    [ProducesResponseType(404, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(422, Type = typeof(ApiBaseResponse))]
    public async Task<ActionResult<PatchUserResponseDTO>> Patch([FromBody] PatchUserRequestDTO patchUserRequestDto)
    {
        var result = await _applicationServiceUser.EditUser(patchUserRequestDto);
        return await Return(result);
    }
}

