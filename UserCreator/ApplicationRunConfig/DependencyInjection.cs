using Microsoft.EntityFrameworkCore;
using UserCreator.Application.Services;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.RepositoriesInterfaces;
using UserCreator.Infrastructure.AppContext;
using UserCreator.Infrastructure.Repositories;

namespace UserCreator.ApplicationRunConfig;

public static class DependencyInjection
{
    public static void ConfigureDependencies(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}

