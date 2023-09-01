using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateChangeUserDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;

    public ValidateChangeUserDataMiddleware(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }

    public void Validate(BaseEntity user)
    {
        var instance = user as User;

    }
}

