using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares
{
    public interface IValidationMiddleware
    {
        Task Validate(BaseEntity apiBaseRequest);
    }
}
