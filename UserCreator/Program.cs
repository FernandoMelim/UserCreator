using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCreator.ApplicationRunConfig;
using UserCreator.Domain.DTOs.Responses;

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

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyInjection.ConfigureDependencies(builder.Services);
DataBaseConfig.ConfigureDatabases(builder.Services, builder.Configuration);


builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/healthz");

DataBaseConfig.ExecuteMigrations(builder.Configuration, app);
MiddlewaresConfig.ConfigureMiddlewares(app);
CorsConfig.ConfigureCors(app);


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
