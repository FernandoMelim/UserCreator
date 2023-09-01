using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateChangeAddressDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;

    public ValidateChangeAddressDataMiddleware(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));

    }

    public void Validate(BaseEntity user)
    {
        var instance = (user as User).Adresses;
    }
}

