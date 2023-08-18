using System.Net;
using System.Text.Json;
using UserCreator.Domain.DTOs;
using UserCreator.Domain.DTOs.Responses;
using UserCreator.Infrastructure.Exceptions;

namespace UserCreator.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var result = new ApiBaseResponse();

            switch (error)
            {
                case ObjectNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Errors.Add("Item não foi encontrado no banco.");
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result.StatusCode = HttpStatusCode.InternalServerError;
                    result.Errors.Add("Ocorreu um erro no servidor");
                    break;
            }

            await response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}

