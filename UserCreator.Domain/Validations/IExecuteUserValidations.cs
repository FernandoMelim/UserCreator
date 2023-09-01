using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations;

public interface IExecuteUserValidations
{
    Task ExecuteUserSaveValidation(User postUserRequestDto);

}

