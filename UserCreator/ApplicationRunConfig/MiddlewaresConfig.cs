using UserCreator.ApplicationRunConfig.Middlewares;

namespace UserCreator.ApplicationRunConfig
{
    public static class MiddlewaresConfig
    {
        public static void ConfigureMiddlewares(WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
