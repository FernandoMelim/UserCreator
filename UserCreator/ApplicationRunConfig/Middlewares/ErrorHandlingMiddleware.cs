using System.Net;
using System.Text.Json;
using UserCreator.Application.DTOs.Responses;
using UserCreator.Infrastructure.Exceptions;

namespace UserCreator.ApplicationRunConfig.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        var result = new ApiBaseResponse();

        try
        {
            await _next(context);
        }
        catch (ObjectNotFoundException)
        {
            response.StatusCode = (int)HttpStatusCode.NotFound;
            result.StatusCode = HttpStatusCode.NotFound;
            result.Errors = new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("ObjectNotFoundException", new List<string>() { "Item não foi encontrado no banco." })
            };

            await response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (Exception)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            result.StatusCode = HttpStatusCode.InternalServerError;
            result.Errors = new List<KeyValuePair<string, List<string>>>()
            {
                new KeyValuePair<string, List<string>>("InternalServerError", new List<string>() { "Ocorreu um erro no servidor." })
            };

            await response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}

