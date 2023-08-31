using UserCreator.Domain.Entities;
using UserCreator.Domain.Enums;
using UserCreator.Domain.Validations;
using UserCreator.Domain.Validations.Middlewares;

namespace UserCreator.Tests.UserCreator.Domain.Validations;

public class ExecuteUserValidationsTests
{
    public ExecuteUserValidationsTests()
    {

    }

    [Fact]
    public void ExecuteUserSaveValidation_ValidDto_CallsAllMiddlewaresAndClearsList()
    {
        // Arrange
        var validationNotificator = new ValidationNotifications();

        var validateCreateUserDataMiddleware = new ValidateCreateUserDataMiddleware(validationNotificator);
        var validateCreateAddressDataMiddleware = new ValidateCreateAddressDataMiddleware(validationNotificator);
        var validateChangeUserDataMiddleware = new ValidateChangeUserDataMiddleware(validationNotificator);
        var validateChangeAddressDataMiddleware = new ValidateChangeAddressDataMiddleware(validationNotificator);

        var executeUserValidations = new ExecuteUserValidations(
            validateCreateUserDataMiddleware,
            validateCreateAddressDataMiddleware,
            validateChangeUserDataMiddleware,
            validateChangeAddressDataMiddleware
        );

        var user = new User()
        {
            Name = "name",
            BirthDate = DateTime.Now,
            Email = "email",
            Phone = "232342ddd3",
            SchoolingLevel = (int)SchoolingLevelEnum.Elementary,
            Adresses = new List<Address>
            {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "12345",
                    State = "SP",
                    Street = "stree"
                }
            }
        };

        // Act
        executeUserValidations.ExecuteUserSaveValidation(user);

        var errors = validationNotificator.GetErrors();

        // Assert
        Assert.True(errors.ContainsKey("Email"));
        Assert.True(errors.ContainsKey("Phone"));
        Assert.True(errors.ContainsKey("Address.State"));
        Assert.True(errors.ContainsKey("Address.PostalCode"));
    }

    [Fact]
    public void ExecuteUserChangeValidation_ValidDto_CallsAllMiddlewaresAndClearsList()
    {
        // Arrange
        var validationNotificator = new ValidationNotifications();

        var validateCreateUserDataMiddleware = new ValidateCreateUserDataMiddleware(validationNotificator);
        var validateCreateAddressDataMiddleware = new ValidateCreateAddressDataMiddleware(validationNotificator);
        var validateChangeUserDataMiddleware = new ValidateChangeUserDataMiddleware(validationNotificator);
        var validateChangeAddressDataMiddleware = new ValidateChangeAddressDataMiddleware(validationNotificator);

        var executeUserValidations = new ExecuteUserValidations(
            validateCreateUserDataMiddleware,
            validateCreateAddressDataMiddleware,
            validateChangeUserDataMiddleware,
            validateChangeAddressDataMiddleware
        );

        var patchUserRequestDto = new User()
        {
            Name = "name",
            BirthDate = DateTime.Now,
            Email = "email",
            Phone = "232342ddd3",
            SchoolingLevel = (int)SchoolingLevelEnum.Elementary,
            Adresses = new List<Address>
            {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "12345",
                    State = "SP",
                    Street = "stree"
                }
            }
        };

        // Act
        executeUserValidations.ExecuteUserChangeValidation(patchUserRequestDto);

        var errors = validationNotificator.GetErrors();

        // Assert
        Assert.True(errors.ContainsKey("Id"));
        Assert.True(errors.ContainsKey("Email"));
        Assert.True(errors.ContainsKey("Phone"));
        Assert.True(errors.ContainsKey("Address.Id"));
        Assert.True(errors.ContainsKey("Address.State"));
        Assert.True(errors.ContainsKey("Address.PostalCode"));
    }
}

