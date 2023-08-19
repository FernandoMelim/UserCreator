namespace UserCreator.ApplicationRunConfig
{
    public static class CorsConfig
    {
        public static void ConfigureCors(WebApplication app)
        {
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
        }
    }
}
