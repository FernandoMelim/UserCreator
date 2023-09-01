using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Domain.Validations;
using UserCreator.Domain.Validations.Middlewares;

namespace UserCreator.Tests.UserCreator.Domain.Validations;

public class ExecuteUserValidationsTests
{
    [Fact]
    public async Task ExecuteUserSaveValidation_WithValidUser_NoErrors()
    {
        // Arrange
        var mockValidationMiddlewareUserData = new Mock<ValidateSaveUserDataMiddleware>(Mock.Of<IValidationNotifications>(), Mock.Of<IUserRepository>());
        var mockValidationMiddlewareAddressData = new Mock<ValidateSaveAddressDataMiddleware>(Mock.Of<IValidationNotifications>(), Mock.Of<IAddressRepository>());

        var executeUserValidations = new ExecuteUserValidations(
            mockValidationMiddlewareUserData.Object,
            mockValidationMiddlewareAddressData.Object
        );

        var user = new User() { Adresses = new List<Address>() };

        // Act
        await executeUserValidations.ExecuteUserSaveValidation(user);

        // Assert
        mockValidationMiddlewareUserData.Verify(middleware => middleware.Validate(user), Times.Once);
        mockValidationMiddlewareAddressData.Verify(middleware => middleware.Validate(user), Times.Once);
    }

    [Fact]
    public async Task ExecuteUserSaveValidation_UserValidationErrors_ValidationNotificationsContainsErrors()
    {
        // Arrange
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        var mockValidationMiddlewareUserData = new Mock<ValidateSaveUserDataMiddleware>(mockValidationNotifications.Object, Mock.Of<IUserRepository>());
        var mockValidationMiddlewareAddressData = new Mock<ValidateSaveAddressDataMiddleware>(Mock.Of<IValidationNotifications>(), Mock.Of<IAddressRepository>());

        mockValidationMiddlewareUserData.Setup(middleware => middleware.Validate(It.IsAny<User>()))
            .Callback(() => mockValidationNotifications.Object.AddError("Name", "User name is invalid"));

        var executeUserValidations = new ExecuteUserValidations(
            mockValidationMiddlewareUserData.Object,
            mockValidationMiddlewareAddressData.Object
        );

        var user = new User() { Adresses = new List<Address>() };

        // Act
        await executeUserValidations.ExecuteUserSaveValidation(user);

        // Assert
        mockValidationNotifications.Verify(validationNotifications => validationNotifications.AddError("Name", "User name is invalid"), Times.Once);
    }

    [Fact]
    public async Task ExecuteUserSaveValidation_AddressValidationErrors_ValidationNotificationsContainsErrors()
    {
        // Arrange
        var mockValidationNotifications = new Mock<IValidationNotifications>();
        var mockValidationMiddlewareUserData = new Mock<ValidateSaveUserDataMiddleware>(Mock.Of<IValidationNotifications>(), Mock.Of<IUserRepository>());
        var mockValidationMiddlewareAddressData = new Mock<ValidateSaveAddressDataMiddleware>(mockValidationNotifications.Object, Mock.Of<IAddressRepository>());

        mockValidationMiddlewareAddressData.Setup(middleware => middleware.Validate(It.IsAny<User>()))
            .Callback(() => mockValidationNotifications.Object.AddError("Address[12345]", "Address is invalid"));

        var executeUserValidations = new ExecuteUserValidations(
            mockValidationMiddlewareUserData.Object,
            mockValidationMiddlewareAddressData.Object
        );

        var user = new User() { Adresses = new List<Address>() };

        // Act
        await executeUserValidations.ExecuteUserSaveValidation(user);

        // Assert
        mockValidationNotifications.Verify(validationNotifications => validationNotifications.AddError("Address[12345]", "Address is invalid"), Times.Once);
    }
}
