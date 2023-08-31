using AutoMapper;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.Entities;
using UserCreator.Domain.RepositoriesInterfaces;
using UserCreator.Domain.Validations;

namespace UserCreator.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IExecuteUserValidations _executeUserValidations;
    private readonly IValidationNotifications _validationNotifications;

    public UserService(IUserRepository userRepository, IMapper mapper, IExecuteUserValidations executeUserValidations, IValidationNotifications validationNotifications)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _executeUserValidations = executeUserValidations ?? throw new ArgumentNullException(nameof(executeUserValidations));
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }

    public async Task CreateUser(PostUserRequestDTO postUserRequestDto)
    {
        _executeUserValidations.ExecuteUserSaveValidation(postUserRequestDto);
        if (!_validationNotifications.HasErrors())
        {
            var user = _mapper.Map<User>(postUserRequestDto);
            await _userRepository.CreateUser(user);
        }
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);
    }

    public async Task EditUser(PatchUserRequestDTO patchUserRequestDto)
    {
        _executeUserValidations.ExecuteUserChangeValidation(patchUserRequestDto);
        if (!_validationNotifications.HasErrors())
        {
            var user = _mapper.Map<User>(patchUserRequestDto);
            await _userRepository.EditUser(user);
        }
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var allUsers = await _userRepository.GetAllUsers();
        return allUsers;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _userRepository.GetUser(id);
        return user;
    }
}

