using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Domain.Validations;
using UserCreator.Domain.Validations.Middlewares;

namespace UserCreator.Tests.UserCreator.Domain.Validations.Middlewares;

public class ValidateSaveUserDataMiddlewareTests
{
    [Fact]
    public async Task Validate_UserDoesNotExist_NoValidationErrors()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.UserExistsInDatabase(It.IsAny<string>())).ReturnsAsync(false);

        var validationNotificationsMock = new Mock<IValidationNotifications>();

        var user = new User { Name = "NewUser" };

        var middleware = new ValidateSaveUserDataMiddleware(validationNotificationsMock.Object, userRepositoryMock.Object);

        // Act
        await middleware.Validate(user);

        // Assert
        validationNotificationsMock.Verify(validation => validation.AddError(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Validate_UserExists_ValidationErrorsAdded()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.UserExistsInDatabase(It.IsAny<string>())).ReturnsAsync(true);

        var validationNotificationsMock = new Mock<IValidationNotifications>();
        validationNotificationsMock.Setup(validation => validation.AddError(It.IsAny<string>(), It.IsAny<string>()));

        var user = new User { Name = "ExistingUser" };

        var middleware = new ValidateSaveUserDataMiddleware(validationNotificationsMock.Object, userRepositoryMock.Object);

        // Act
        await middleware.Validate(user);

        // Assert
        validationNotificationsMock.Verify(validation => validation.AddError("Name", "Já existe um usuário cadastrado com esse nome."), Times.Once);
    }
}

