using UserCreator.Application.ApplicationServices;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Domain.Interfaces.Services;
using UserCreator.Domain.Services;
using UserCreator.Domain.Validations;
using UserCreator.Domain.Validations.Middlewares;
using UserCreator.Infrastructure.Repositories;

namespace UserCreator.ApplicationRunConfig;

public static class DependencyInjection
{
    public static void ConfigureDependencies(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidationNotifications, ValidationNotifications>();

        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IApplicationServiceUser, ApplicationServiceUser>();

        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        serviceCollection.AddScoped<ValidateCreateUserDataMiddleware>();
        serviceCollection.AddScoped<ValidateCreateAddressDataMiddleware>();
        serviceCollection.AddScoped<ValidateChangeUserDataMiddleware>();
        serviceCollection.AddScoped<ValidateChangeAddressDataMiddleware>();
        serviceCollection.AddScoped<IExecuteUserValidations, ExecuteUserValidations>();

        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}

