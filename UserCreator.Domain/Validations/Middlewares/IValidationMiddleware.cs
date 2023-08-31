using UserCreator.Domain.DTOs.Requets;

namespace UserCreator.Domain.Validations.Middlewares
{
    public interface IValidationMiddleware
    {
        void Validate(ApiBaseRequest apiBaseRequest);
    }
}
