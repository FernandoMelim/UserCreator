using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations;

public interface IExecuteUserValidations
{
    void ExecuteUserSaveValidation(User postUserRequestDto);

    void ExecuteUserChangeValidation(User patchUserRequestDto);
}

