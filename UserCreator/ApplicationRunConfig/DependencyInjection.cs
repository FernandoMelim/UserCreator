using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using UserCreator.Application.ApplicationServices;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DtoValidators;
using UserCreator.Domain.Entities;
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
        serviceCollection.AddScoped<IAddressRepository, AddressRepository>();

        serviceCollection.AddScoped<ValidateSaveUserDataMiddleware>();
        serviceCollection.AddScoped<ValidateSaveAddressDataMiddleware>();
        serviceCollection.AddScoped<IExecuteUserValidations, ExecuteUserValidations>();

        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        AddFluentValidationValidators(serviceCollection);
    }

    private static void AddFluentValidationValidators(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<PostUserRequestDTO>, PostUserRequestValidator>();
        serviceCollection.AddScoped<IValidator<PatchUserRequestDTO>, PatchUserRequestValidator>();

        serviceCollection.AddScoped<IValidator<ChangeAddressRequestDTO>, ChangeAddressRequestValidator>();
        serviceCollection.AddScoped<IValidator<CreateAddressRequestDTO>, CreateAddressRequestValidator>();
    }
}

