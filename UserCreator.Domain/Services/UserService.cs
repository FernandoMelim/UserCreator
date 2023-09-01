using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Domain.Interfaces.Services;
using UserCreator.Domain.Validations;

namespace UserCreator.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IExecuteUserValidations _executeUserValidations;
    private readonly IValidationNotifications _validationNotifications;

    public UserService(IUserRepository userRepository, IExecuteUserValidations executeUserValidations, IValidationNotifications validationNotifications)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
        _executeUserValidations = executeUserValidations ?? throw new ArgumentNullException(nameof(executeUserValidations));
    }

    public async Task CreateUser(User user)
    {
        await _executeUserValidations.ExecuteUserSaveValidation(user);
        if (!_validationNotifications.HasErrors())
            await _userRepository.CreateUser(user);
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);
    }

    public async Task EditUser(User user)
    {
        await _executeUserValidations.ExecuteUserSaveValidation(user);
        if (!_validationNotifications.HasErrors())
            await _userRepository.EditUser(user);
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

