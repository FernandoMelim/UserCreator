

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UserCreator.Application.Services;
using UserCreator.Application.Validations;
using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.Entities;
using UserCreator.Domain.RepositoriesInterfaces;
using AutoMapper;
using UserCreator.Infrastructure.DtosEntitiesMappers;

namespace UserCreator.Tests.UserCreator.Application.Services;



public class UserServiceTests
{

    public UserServiceTests()
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
    public async Task CreateUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockValidationExecutor = new Mock<IExecuteUserValidations>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var userDto = new PostUserRequestDTO();

        // Act
        await userService.CreateUser(userDto);

        // Assert
        mockRepository.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task CreateUserFailValidation()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockValidationExecutor = new Mock<IExecuteUserValidations>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(true);

        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var userDto = new PostUserRequestDTO();

        // Act
        await userService.CreateUser(userDto);

        // Assert
        mockRepository.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task EditUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockValidationExecutor = new Mock<IExecuteUserValidations>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(false);

        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var userDto = new PatchUserRequestDTO();

        // Act
        await userService.EditUser(userDto);

        // Assert
        mockRepository.Verify(repo => repo.EditUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task EditUserValidationFail()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockValidationExecutor = new Mock<IExecuteUserValidations>();
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        mockValidationNotifications.Setup(n => n.HasErrors()).Returns(true);

        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var userDto = new PatchUserRequestDTO();

        // Act
        await userService.EditUser(userDto);

        // Assert
        mockRepository.Verify(repo => repo.EditUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task DeleteUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            Mock.Of<IExecuteUserValidations>(),
            Mock.Of<IValidationNotifications>()
        );

        int userIdToDelete = 123; // Replace with a valid user ID

        // Act
        await userService.DeleteUser(userIdToDelete);

        // Assert
        mockRepository.Verify(repo => repo.DeleteUser(userIdToDelete), Times.Once);
    }

    [Fact]
    public async Task GetAllUsers()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            Mock.Of<IExecuteUserValidations>(),
            Mock.Of<IValidationNotifications>()
        );

        var users = new List<User>
            {
                new User(),
                new User(),
                new User()
            };
        mockRepository.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

        // Act
        var retrievedUsers = await userService.GetAllUsers();

        // Assert
        Assert.Equal(users.Count, retrievedUsers.Count());
    }

    [Fact]
    public async Task GetUserById()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var userService = new UserService(
            mockRepository.Object,
            CreateMapper(),
            Mock.Of<IExecuteUserValidations>(),
            Mock.Of<IValidationNotifications>()
        );

        var user = new User();
        mockRepository.Setup(repo => repo.GetUser(It.IsAny<int>())).ReturnsAsync(user);

        int userId = 123;

        // Act
        var retrievedUser = await userService.GetUserById(userId);

        // Assert
        Assert.Equal(user, retrievedUser);
    }

}


