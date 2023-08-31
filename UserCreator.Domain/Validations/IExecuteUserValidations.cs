using UserCreator.Domain.DTOs.Requets.User;

namespace UserCreator.Domain.Validations;

public interface IExecuteUserValidations
{
    void ExecuteUserSaveValidation(PostUserRequestDTO postUserRequestDto);

    void ExecuteUserChangeValidation(PatchUserRequestDTO patchUserRequestDto);
}

