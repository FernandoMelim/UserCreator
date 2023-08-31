using AutoMapper;
using UserCreator.Application.ApplicationServices;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Services;

namespace UserCreator.Tests.UserCreator.Application.ApplicationServices;

public class ApplicationServiceUserTests
{
    [Fact]
    public async Task CreateUser_ValidRequest_Success()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var userServiceMock = new Mock<IUserService>();
        var applicationService = new ApplicationServiceUser(mapperMock.Object, userServiceMock.Object);
        var postUserRequestDto = new PostUserRequestDTO();

        // Act
        await applicationService.CreateUser(postUserRequestDto);

        // Assert
        userServiceMock.Verify(s => s.CreateUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_ValidId_Success()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var userServiceMock = new Mock<IUserService>();
        var applicationService = new ApplicationServiceUser(mapperMock.Object, userServiceMock.Object);
        var userId = 1;

        // Act
        await applicationService.DeleteUser(userId);

        // Assert
        userServiceMock.Verify(s => s.DeleteUser(userId), Times.Once);
    }

    [Fact]
    public async Task EditUser_ValidRequest_Success()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var userServiceMock = new Mock<IUserService>();
        var applicationService = new ApplicationServiceUser(mapperMock.Object, userServiceMock.Object);
        var patchUserRequestDto = new PatchUserRequestDTO();

        // Act
        await applicationService.EditUser(patchUserRequestDto);

        // Assert
        userServiceMock.Verify(s => s.EditUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsUsers_Success()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var userServiceMock = new Mock<IUserService>();
        var applicationService = new ApplicationServiceUser(mapperMock.Object, userServiceMock.Object);
        var users = new List<User> { new User(), new User() };
        userServiceMock.Setup(s => s.GetAllUsers()).ReturnsAsync(users);

        // Act
        var result = await applicationService.GetAllUsers();

        // Assert
        Assert.Equal(users, result);
    }

    [Fact]
    public async Task GetUserById_ValidId_ReturnsUser()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var userServiceMock = new Mock<IUserService>();
        var applicationService = new ApplicationServiceUser(mapperMock.Object, userServiceMock.Object);
        var userId = 1;
        var user = new User { Id = userId };
        userServiceMock.Setup(s => s.GetUserById(userId)).ReturnsAsync(user);

        // Act
        var result = await applicationService.GetUserById(userId);

        // Assert
        Assert.Equal(user, result);
    }
}


