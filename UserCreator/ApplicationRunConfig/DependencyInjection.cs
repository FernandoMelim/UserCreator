using UserCreator.Application.Services;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Application.Validations;
using UserCreator.Application.Validations.Middlewares;
using UserCreator.Domain.RepositoriesInterfaces;
using UserCreator.Infrastructure.Repositories;

namespace UserCreator.ApplicationRunConfig;

public static class DependencyInjection
{
    public static void ConfigureDependencies(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidationNotifications, ValidationNotifications>();

        serviceCollection.AddScoped<IUserService, UserService>();

        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        serviceCollection.AddScoped<ValidateCreateUserDataMiddleware>();
        serviceCollection.AddScoped<ValidateCreateAddressDataMiddleware>();
        serviceCollection.AddScoped<ValidateChangeUserDataMiddleware>();
        serviceCollection.AddScoped<ValidateChangeAddressDataMiddleware>();
        serviceCollection.AddScoped<IExecuteUserValidations, ExecuteUserValidations>();

        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}

