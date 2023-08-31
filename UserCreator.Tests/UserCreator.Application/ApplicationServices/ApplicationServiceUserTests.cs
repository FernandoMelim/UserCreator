using AutoMapper;
using UserCreator.Application.ApplicationServices;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
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
        var users = new List<User> { new User() { Id = 1 }, new User() { Id = 2 } };

        mapperMock.Setup(s => s.Map<List<UserResponseDTO>>(users)).Returns(new List<UserResponseDTO>() { new UserResponseDTO() { Id = 1 }, new UserResponseDTO() { Id = 2 } });
        userServiceMock.Setup(s => s.GetAllUsers()).ReturnsAsync(users);

        // Act
        var result = await applicationService.GetAllUsers();

        // Assert
        Assert.Contains(1, result.Users.Select(x => x.Id));
        Assert.Contains(2, result.Users.Select(x => x.Id));

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

        mapperMock.Setup(s => s.Map<UserResponseDTO>(user)).Returns(new UserResponseDTO() { Id = userId });
        userServiceMock.Setup(s => s.GetUserById(userId)).ReturnsAsync(user);

        // Act
        var result = await applicationService.GetUserById(userId);

        // Assert
        Assert.Equal(user.Id, result.User.Id);
    }
}


