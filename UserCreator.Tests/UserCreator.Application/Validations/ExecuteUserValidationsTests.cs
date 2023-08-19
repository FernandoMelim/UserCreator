using UserCreator.Application.Validations.Middlewares;
using UserCreator.Application.Validations;
using UserCreator.Domain.DTOs.Requets.User;

namespace UserCreator.Tests.UserCreator.Application.Validations;

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

        var postUserRequestDto = new PostUserRequestDTO()
        {
            Name = "name",
            BirthDate = DateTime.Now,
            Email = "email",
            Phone = "232342ddd3",
            SchoolingLevel = Domain.Enums.SchoolingLevelEnum.Elementary,
            Adresses = new List<CreateAddressRequestDTO> 
            {
                new CreateAddressRequestDTO()
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
        executeUserValidations.ExecuteUserSaveValidation(postUserRequestDto);

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

        var patchUserRequestDto = new PatchUserRequestDTO()
        {
            Name = "name",
            BirthDate = DateTime.Now,
            Email = "email",
            Phone = "232342ddd3",
            SchoolingLevel = Domain.Enums.SchoolingLevelEnum.Elementary,
            Adresses = new List<ChangeAddressRequestDTO>
            {
                new ChangeAddressRequestDTO()
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

