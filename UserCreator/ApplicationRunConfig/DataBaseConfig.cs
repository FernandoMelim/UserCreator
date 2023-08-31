using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UserCreator.Infrastructure.AppContext;

namespace UserCreator.ApplicationRunConfig;

public static class DataBaseConfig
{

    public static void ConfigureDatabases(IServiceCollection serviceCollection, ConfigurationManager configManager)
    {
        serviceCollection.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configManager.GetConnectionString("DefaultConnection"),
                                                             b => b.MigrationsAssembly("UserCreator.Infrastructure")));
    }

    public static void ExecuteMigrations(ConfigurationManager configManager, WebApplication app)
    {
        if (Boolean.Parse(configManager.GetSection("ExecuteMigrations").Value))
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                db.Database.Migrate();
            }
        }
    }
}

