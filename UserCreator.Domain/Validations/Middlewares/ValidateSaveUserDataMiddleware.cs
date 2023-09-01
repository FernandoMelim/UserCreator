using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateSaveUserDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;
    private readonly IUserRepository _userRepository;


    public ValidateSaveUserDataMiddleware(
        IValidationNotifications validationNotifications,
        IUserRepository userRepository
        )
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async virtual Task Validate(BaseEntity user)
    {
        var instance = user as User;

        var userExists = await  _userRepository.UserExistsInDatabase(instance.Name);
        if (userExists)
            _validationNotifications.AddError("Name", "Já existe um usuário cadastrado com esse nome.");
    }
}

