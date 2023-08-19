using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserCreator.Application.Services;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Domain.DTOs.Responses;
using UserCreator.Domain.RepositoriesInterfaces;
using UserCreator.Infrastructure.AppContext;
using UserCreator.Infrastructure.Repositories;
using UserCreator.Middlewares;

var builder = WebApplication.CreateBuilder(args);



builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var response = new ApiBaseResponse
            {
                StatusCode = HttpStatusCode.UnprocessableEntity
            };

            foreach (var (key, value) in context.ModelState)
            {
                value.Errors.Select(error => error.ErrorMessage).ToList().ForEach(error =>
                {
                    response.Errors.Add(error);

                });
            }

            return new BadRequestObjectResult(response);
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/healthz");

if (Boolean.Parse(builder.Configuration.GetSection("ExecuteMigrations").Value))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        db.Database.Migrate();
    }
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
