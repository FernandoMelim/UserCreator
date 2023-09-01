using AutoMapper;
using FluentValidation;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Services;
using UserCreator.Domain.Validations;

namespace UserCreator.Application.ApplicationServices;

public class ApplicationServiceUser : IApplicationServiceUser
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IValidator<PostUserRequestDTO> _postUserRequestValidator;
    private readonly IValidator<PatchUserRequestDTO> _patchUserRequestValidator;
    private readonly IValidationNotifications _validationNotifications;


    public ApplicationServiceUser(
        IMapper mapper, 
        IUserService userService,
        IValidator<PostUserRequestDTO> postUserRequestValidator,
        IValidator<PatchUserRequestDTO> patchUserRequestValidator,
        IValidationNotifications validationNotifications)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _postUserRequestValidator = postUserRequestValidator ?? throw new ArgumentNullException(nameof(postUserRequestValidator));
        _patchUserRequestValidator = patchUserRequestValidator ?? throw new ArgumentNullException(nameof(patchUserRequestValidator));
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }

    public async Task<PostUserResponseDTO> CreateUser(PostUserRequestDTO postUserRequestDto)
    {
        var validationResult = await _postUserRequestValidator.ValidateAsync(postUserRequestDto);
        var response = new PostUserResponseDTO();

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                _validationNotifications.AddError(error.PropertyName, error.ErrorMessage);

            return response;
        }

        var user = _mapper.Map<User>(postUserRequestDto);
        await _userService.CreateUser(user);
        return response;
    }

    public async Task<DeleteUserResponseDTO> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return new DeleteUserResponseDTO();
    }

    public async Task<PatchUserResponseDTO> EditUser(PatchUserRequestDTO patchUserRequestDto)
    {

        var validationResult = await _patchUserRequestValidator.ValidateAsync(patchUserRequestDto);
        var response = new PatchUserResponseDTO();

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                _validationNotifications.AddError(error.PropertyName, error.ErrorMessage);

            return response;
        }

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

