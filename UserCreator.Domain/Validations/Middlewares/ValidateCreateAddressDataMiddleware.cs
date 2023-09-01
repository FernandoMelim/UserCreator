using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateCreateAddressDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;

    public ValidateCreateAddressDataMiddleware(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));

    }

    public void Validate(BaseEntity user)
    {
        var addresses = (user as User).Adresses;

    }
}

