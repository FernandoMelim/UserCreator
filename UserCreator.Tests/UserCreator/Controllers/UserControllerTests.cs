using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCreator.Application.ServicesInterfaces;
using UserCreator.Application.Validations;
using UserCreator.Controllers;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;
using UserCreator.Infrastructure.DtosEntitiesMappers;

namespace UserCreator.Tests.UserCreator.Controllers;

public class UserControllerTests
{
    public UserControllerTests()
    {

    }


    private IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMapper>();
            cfg.AddProfile<AddressMapper>();
        });

        return configuration.CreateMapper();
    }

    [Fact]
    public async Task GetUser()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        int userId = 123;
        var user = new User();
        mockService.Setup(service => service.GetUserById(userId)).ReturnsAsync(user);

        // Act
        var result = await controller.Get(userId);

        // Assert
        var response = Assert.IsType<ActionResult<GetUserResponseDTO>>(result);
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(HttpStatusCode.OK, ((ApiBaseResponse)((ObjectResult)result.Result).Value).StatusCode);
    }

    [Fact]
    public async Task GetAllUsers()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        var users = new List<User> { new User(), new User() };
        mockService.Setup(service => service.GetAllUsers()).ReturnsAsync(users);

        // Act
        var result = await controller.Get();

        // Assert
        var response = Assert.IsType<ActionResult<GetAllUsersResponseDTO>>(result);
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(HttpStatusCode.OK, ((ApiBaseResponse)((ObjectResult)result.Result).Value).StatusCode);
    }

    [Fact]
    public async Task Post_ValidDto_UserCreated_ReturnsPostUserResponse()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        var userDto = new PostUserRequestDTO(); // Create a user DTO

        // Act
        var result = await controller.Post(userDto);

        // Assert
        var response = Assert.IsType<ActionResult<PostUserResponseDTO>>(result);
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(HttpStatusCode.OK, ((ApiBaseResponse)((ObjectResult)result.Result).Value).StatusCode);
    }

    [Fact]
    public async Task Delete_ValidId_UserDeleted_ReturnsDeleteUserResponse()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        int userId = 123; // Replace with a valid user ID

        // Act
        var result = await controller.Delete(userId);

        // Assert
        var response = Assert.IsType<ActionResult<DeleteUserResponseDTO>>(result);
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(HttpStatusCode.OK, ((ApiBaseResponse)((ObjectResult)result.Result).Value).StatusCode);
    }

    [Fact]
    public async Task Patch_ValidDto_UserEdited_ReturnsPatchUserResponse()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        var patchUserDto = new PatchUserRequestDTO(); // Create a patch user DTO

        // Act
        var result = await controller.Patch(patchUserDto);

        // Assert
        var response = Assert.IsType<ActionResult<PatchUserResponseDTO>>(result);
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(HttpStatusCode.OK, ((ApiBaseResponse)((ObjectResult)result.Result).Value).StatusCode);
    }

}

