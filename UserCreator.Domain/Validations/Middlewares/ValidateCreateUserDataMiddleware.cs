using System.Text.RegularExpressions;
using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateCreateUserDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;
    private string _emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private string _phonePattern = @"^\+?[0-9]*$";

    public ValidateCreateUserDataMiddleware(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }

    public void Validate(BaseEntity user)
    {
        var instance = user as User;
    }
}

