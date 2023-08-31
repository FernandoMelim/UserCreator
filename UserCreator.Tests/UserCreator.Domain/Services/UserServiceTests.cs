using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Domain.Services;
using UserCreator.Domain.Validations;

namespace UserCreator.Tests.UserCreator.Domain.Services;



public class UserServiceTests
{

    public UserServiceTests()
    {

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
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var user = new User();

        // Act
        await userService.CreateUser(user);

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
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var user = new User();

        // Act
        await userService.CreateUser(user);

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
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var user = new User();

        // Act
        await userService.EditUser(user);

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
            mockValidationExecutor.Object,
            mockValidationNotifications.Object
        );

        var user = new User();

        // Act
        await userService.EditUser(user);

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
            Mock.Of<IExecuteUserValidations>(),
            Mock.Of<IValidationNotifications>()
        );

        int userIdToDelete = 123;

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


