using UserCreator.Domain.Validations;

namespace UserCreator.Tests.UserCreator.Domain.Validations;

public class ValidationNotificationsTests
{
    public ValidationNotificationsTests()
    {

    }

    [Fact]
    public void AddError_AddsErrorForKey()
    {
        // Arrange
        var validationNotifications = new ValidationNotifications();
        var key = "field";
        var error = "error message";

        // Act
        validationNotifications.AddError(key, error);

        // Assert
        Assert.True(validationNotifications.HasErrors());
        var errors = validationNotifications.GetErrors();
        Assert.Contains(key, errors.Keys);
        Assert.Contains(error, errors[key]);
    }

    [Fact]
    public void HasErrors_NoErrors_ReturnsFalse()
    {
        // Arrange
        var validationNotifications = new ValidationNotifications();

        // Act & Assert
        Assert.False(validationNotifications.HasErrors());
    }

    [Fact]
    public void HasErrors_WithErrors_ReturnsTrue()
    {
        // Arrange
        var validationNotifications = new ValidationNotifications();
        validationNotifications.AddError("field", "error");

        // Act & Assert
        Assert.True(validationNotifications.HasErrors());
    }

    [Fact]
    public void GetErrors_ReturnsErrors()
    {
        // Arrange
        var validationNotifications = new ValidationNotifications();
        validationNotifications.AddError("field", "error");

        // Act
        var errors = validationNotifications.GetErrors();

        // Assert
        Assert.Single(errors);
        Assert.Contains("field", errors.Keys);
        Assert.Contains("error", errors["field"]);
    }
}

