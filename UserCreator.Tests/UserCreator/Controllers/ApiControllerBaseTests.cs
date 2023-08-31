using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCreator.Application.DTOs.Responses;
using UserCreator.Controllers;
using UserCreator.Domain.Validations;

namespace UserCreator.Tests.UserCreator.Controllers;

public class ApiControllerBaseTests
{
    public ApiControllerBaseTests()
    {

    }

    private class TestApiController : ApiControllerBase
    {
        public TestApiController(IValidationNotifications validationNotifications) : base(validationNotifications)
        {
        }

        public new async Task<ActionResult> Return(ApiBaseResponse apiBaseResponse)
        {
            return await base.Return(apiBaseResponse);
        }
    }

    [Fact]
    public async Task Return_NoValidationErrors_ReturnsOkResult()
    {
        // Arrange
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new TestApiController(mockValidationNotifications.Object);

        var apiResponse = new ApiBaseResponse();

        // Act
        var result = await controller.Return(apiResponse);

        // Assert
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
    }

    [Fact]
    public async Task Return_WithValidationErrors_ReturnsUnprocessableEntityResult()
    {
        // Arrange
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(true);
        var errors = new Dictionary<string, List<string>>
        {
            { "field", new List<string> { "error message" } }
        };

        mockValidationNotifications.Setup(n => n.GetErrors()).Returns(errors);

        var controller = new TestApiController(mockValidationNotifications.Object);

        var apiResponse = new ApiBaseResponse();

        // Act
        var result = await controller.Return(apiResponse);

        // Assert
        var response = Assert.IsType<UnprocessableEntityObjectResult>(result);
        var responseObject = Assert.IsType<ApiBaseResponse>(response.Value);
        Assert.Equal(HttpStatusCode.UnprocessableEntity, (HttpStatusCode)response.StatusCode);
        Assert.Equal(errors, responseObject.Errors);
    }
}

