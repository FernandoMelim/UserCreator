using UserCreator.Domain.Entities;
using UserCreator.Domain.Validations.Middlewares;

namespace UserCreator.Domain.Validations
{
    public class ExecuteUserValidations : IExecuteUserValidations
    {
        private List<IValidationMiddleware> _userMiddlewares;

        private readonly ValidateCreateUserDataMiddleware _validateCreateUserDataMiddleware;
        private readonly ValidateCreateAddressDataMiddleware _validateCreateAddressDataMiddleware;
        private readonly ValidateChangeUserDataMiddleware _validateChangeUserDataMiddleware;
        private readonly ValidateChangeAddressDataMiddleware _validateChangeAddressDataMiddleware;

        public ExecuteUserValidations(
            ValidateCreateUserDataMiddleware validateCreateUserDataMiddleware,
            ValidateCreateAddressDataMiddleware validateCreateAddressDataMiddleware,
            ValidateChangeUserDataMiddleware validateChangeUserDataMiddleware,
            ValidateChangeAddressDataMiddleware validateChangeAddressDataMiddleware
            )
        {
            _userMiddlewares = new List<IValidationMiddleware>();

            _validateCreateUserDataMiddleware = validateCreateUserDataMiddleware ?? throw new ArgumentNullException(nameof(validateCreateUserDataMiddleware));
            _validateCreateAddressDataMiddleware = validateCreateAddressDataMiddleware ?? throw new ArgumentNullException(nameof(validateCreateAddressDataMiddleware));
            _validateChangeUserDataMiddleware = validateChangeUserDataMiddleware ?? throw new ArgumentNullException(nameof(validateChangeUserDataMiddleware));
            _validateChangeAddressDataMiddleware = validateChangeAddressDataMiddleware ?? throw new ArgumentNullException(nameof(validateChangeAddressDataMiddleware));
        }

        private void ConfigureUserSaveValidation()
        {
            _userMiddlewares.Add(_validateCreateUserDataMiddleware);
            _userMiddlewares.Add(_validateCreateAddressDataMiddleware);
        }

        private void ConfigureUserChangeValidation()
        {
            _userMiddlewares.Add(_validateChangeUserDataMiddleware);
            _userMiddlewares.Add(_validateChangeAddressDataMiddleware);
        }

        public void ExecuteUserSaveValidation(User user)
        {
            ConfigureUserSaveValidation();
            foreach (var validationMiddleware in _userMiddlewares)
                validationMiddleware.Validate(user);
            _userMiddlewares.Clear();
        }

        public void ExecuteUserChangeValidation(User user)
        {
            ConfigureUserChangeValidation();
            foreach (var validationMiddleware in _userMiddlewares)
                validationMiddleware.Validate(user);
            _userMiddlewares.Clear();
        }
    }
}
