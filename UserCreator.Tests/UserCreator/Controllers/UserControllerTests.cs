﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserCreator.Application.ApplicationServicesInterfaces;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Application.DtosEntitiesMappers;
using UserCreator.Controllers;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Services;
using UserCreator.Domain.Validations;
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
        var mockService = new Mock<IApplicationServiceUser>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        int userId = 123;
        var user = new GetUserResponseDTO();
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
        var mockService = new Mock<IApplicationServiceUser>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        var users = new GetAllUsersResponseDTO() { Users = new List<UserResponseDTO>() };
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
        var mockService = new Mock<IApplicationServiceUser>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);
        mockService.Setup(s => s.CreateUser(It.IsAny<PostUserRequestDTO>()).Result).Returns(new PostUserResponseDTO());

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        var userDto = new PostUserRequestDTO();

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
        var mockService = new Mock<IApplicationServiceUser>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);
        mockService.Setup(s => s.DeleteUser(It.IsAny<int>()).Result).Returns(new DeleteUserResponseDTO());

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        int userId = 123;

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
        var mockService = new Mock<IApplicationServiceUser>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);
        mockService.Setup(s => s.EditUser(It.IsAny<PatchUserRequestDTO>()).Result).Returns(new PatchUserResponseDTO());

        var controller = new UserController(
            mockService.Object,
            mockValidationNotifications.Object,
            CreateMapper()
        );

        var patchUserDto = new PatchUserRequestDTO();

        // Act
        var result = await controller.Patch(patchUserDto);

        // Assert
        var response = Assert.IsType<ActionResult<PatchUserResponseDTO>>(result);
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(HttpStatusCode.OK, ((ApiBaseResponse)((ObjectResult)result.Result).Value).StatusCode);
    }

}

