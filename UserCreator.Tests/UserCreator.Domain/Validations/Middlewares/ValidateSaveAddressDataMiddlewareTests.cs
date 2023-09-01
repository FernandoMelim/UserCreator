using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Domain.Validations;
using UserCreator.Domain.Validations.Middlewares;

namespace UserCreator.Tests.UserCreator.Domain.Validations.Middlewares;

public class ValidateSaveAddressDataMiddlewareTests
{
    [Fact]
    public async Task Validate_NoAddresses_NoValidationErrors()
    {
        // Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        var validationNotificationsMock = new Mock<IValidationNotifications>();

        var user = new User { Adresses = new List<Address>() };

        var middleware = new ValidateSaveAddressDataMiddleware(validationNotificationsMock.Object, addressRepositoryMock.Object);

        // Act
        await middleware.Validate(user);

        // Assert
        validationNotificationsMock.Verify(validation => validation.AddError(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Validate_NoMatchingAddresses_NoValidationErrors()
    {
        // Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock.Setup(repo => repo.PostalCodeExistsInDatabase(It.IsAny<string>())).ReturnsAsync(false);

        var validationNotificationsMock = new Mock<IValidationNotifications>();

        var user = new User
        {
            Adresses = new List<Address>
                {
                    new Address { PostalCode = "12345" },
                    new Address { PostalCode = "67890" }
                }
        };

        var middleware = new ValidateSaveAddressDataMiddleware(validationNotificationsMock.Object, addressRepositoryMock.Object);

        // Act
        await middleware.Validate(user);

        // Assert
        validationNotificationsMock.Verify(validation => validation.AddError(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Validate_MatchingAddress_ValidationErrorsAdded()
    {
        // Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock.Setup(repo => repo.PostalCodeExistsInDatabase("12345")).ReturnsAsync(true);
        addressRepositoryMock.Setup(repo => repo.PostalCodeExistsInDatabase("67890")).ReturnsAsync(false);

        var validationNotificationsMock = new Mock<IValidationNotifications>();
        validationNotificationsMock.Setup(validation => validation.AddError(It.IsAny<string>(), It.IsAny<string>()));

        var user = new User
        {
            Adresses = new List<Address>
                {
                    new Address { PostalCode = "12345" },
                    new Address { PostalCode = "67890" }
                }
        };

        var middleware = new ValidateSaveAddressDataMiddleware(validationNotificationsMock.Object, addressRepositoryMock.Object);

        // Act
        await middleware.Validate(user);

        // Assert
        validationNotificationsMock.Verify(validation => validation.AddError("Adsress[12345]", "Já existe um endereço cadastrado com CEP."), Times.Once);
        validationNotificationsMock.Verify(validation => validation.AddError("Adsress[67890]", It.IsAny<string>()), Times.Never);
    }
}

