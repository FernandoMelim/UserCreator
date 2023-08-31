using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares
{
    public interface IValidationMiddleware
    {
        void Validate(BaseEntity apiBaseRequest);
    }
}
