using UserCreator.Domain.DTOs.Requets;

namespace UserCreator.Application.Validations.Middlewares
{
    public interface IValidationMiddleware
    {
        void Validate(ApiBaseRequest apiBaseRequest);
    }
}
