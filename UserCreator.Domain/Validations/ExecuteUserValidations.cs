using UserCreator.Domain.Entities;
using UserCreator.Domain.Validations.Middlewares;

namespace UserCreator.Domain.Validations
{
    public class ExecuteUserValidations : IExecuteUserValidations
    {
        private List<IValidationMiddleware> _userMiddlewares;

        private readonly ValidateSaveUserDataMiddleware _validateCreateUserDataMiddleware;
        private readonly ValidateSaveAddressDataMiddleware _validateCreateAddressDataMiddleware;

        public ExecuteUserValidations(
            ValidateSaveUserDataMiddleware validateCreateUserDataMiddleware,
            ValidateSaveAddressDataMiddleware validateCreateAddressDataMiddleware
            )
        {
            _userMiddlewares = new List<IValidationMiddleware>();

            _validateCreateUserDataMiddleware = validateCreateUserDataMiddleware ?? throw new ArgumentNullException(nameof(validateCreateUserDataMiddleware));
            _validateCreateAddressDataMiddleware = validateCreateAddressDataMiddleware ?? throw new ArgumentNullException(nameof(validateCreateAddressDataMiddleware));
        }

        private void ConfigureUserSaveValidation()
        {
            _userMiddlewares.Add(_validateCreateUserDataMiddleware);
            _userMiddlewares.Add(_validateCreateAddressDataMiddleware);
        }

        public async Task ExecuteUserSaveValidation(User user)
        {
            ConfigureUserSaveValidation();
            foreach (var validationMiddleware in _userMiddlewares)
                await validationMiddleware.Validate(user);
            _userMiddlewares.Clear();
        }
    }
}
