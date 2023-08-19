using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCreator.Application.Validations;
using UserCreator.Domain.DTOs.Responses;

namespace UserCreator.Controllers;

public class ApiControllerBase : ControllerBase
{
    private readonly IValidationNotifications _validationNotifications;

    public ApiControllerBase(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }

    protected async virtual Task<ActionResult> Return(ApiBaseResponse apiBaseResponse)
    {
        if (!_validationNotifications.HasErrors())
        {
            apiBaseResponse.StatusCode = HttpStatusCode.OK;
            return Ok(apiBaseResponse);
        }

        var errors = _validationNotifications.GetErrors();
        apiBaseResponse.Errors = new List<KeyValuePair<string, List<string>>>();
        foreach (var error in errors)
            apiBaseResponse.Errors.Add(error);

        apiBaseResponse.StatusCode = HttpStatusCode.UnprocessableEntity;

        return UnprocessableEntity(apiBaseResponse);
    }
}

