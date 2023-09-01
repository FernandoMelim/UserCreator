using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using UserCreator.Application.ApplicationServices;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Application.DTOs.Responses.User;
using UserCreator.Application.DtosEntitiesMappers;
using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Services;
using UserCreator.Domain.Validations;
using UserCreator.Infrastructure.DtosEntitiesMappers;

namespace UserCreator.Tests.ApplicationServices
{
    public class ApplicationServiceUserTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IValidator<PostUserRequestDTO>> _mockPostUserRequestValidator;
        private readonly Mock<IValidator<PatchUserRequestDTO>> _mockPatchUserRequestValidator;
        private readonly Mock<IValidationNotifications> _mockValidationNotifications;

        private readonly ApplicationServiceUser _applicationServiceUser;

        public ApplicationServiceUserTests()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<AddressMapper>(); cfg.AddProfile<UserMapper>(); }).CreateMapper();
            _mockUserService = new Mock<IUserService>();
            _mockPostUserRequestValidator = new Mock<IValidator<PostUserRequestDTO>>();
            _mockPatchUserRequestValidator = new Mock<IValidator<PatchUserRequestDTO>>();
            _mockValidationNotifications = new Mock<IValidationNotifications>();

            _applicationServiceUser = new ApplicationServiceUser(
                _mapper,
                _mockUserService.Object,
                _mockPostUserRequestValidator.Object,
                _mockPatchUserRequestValidator.Object,
                _mockValidationNotifications.Object
            );
        }

        [Fact]
        public async Task CreateUser_WithValidDto_ReturnsPostUserResponseDTO()
        {
            // Arrange
            var postUserRequestDto = new PostUserRequestDTO();
            _mockPostUserRequestValidator.Setup(validator => validator.ValidateAsync(postUserRequestDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUserService.Setup(service => service.CreateUser(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _applicationServiceUser.CreateUser(postUserRequestDto);

            // Assert
            result.Should().BeOfType<PostUserResponseDTO>();
        }

        [Fact]
        public async Task DeleteUser_WithValidId_ReturnsDeleteUserResponseDTO()
        {
            // Arrange
            int userId = 123;
            _mockUserService.Setup(service => service.DeleteUser(userId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _applicationServiceUser.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<DeleteUserResponseDTO>();
        }

        [Fact]
        public async Task EditUser_WithValidDto_ReturnsPatchUserResponseDTO()
        {
            // Arrange
            var patchUserRequestDto = new PatchUserRequestDTO();
            _mockPatchUserRequestValidator.Setup(validator => validator.ValidateAsync(patchUserRequestDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mockUserService.Setup(service => service.EditUser(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _applicationServiceUser.EditUser(patchUserRequestDto);

            // Assert
            result.Should().BeOfType<PatchUserResponseDTO>();
        }

        [Fact]
        public async Task GetAllUsers_ReturnsGetAllUsersResponseDTO()
        {
            // Arrange
            var allUsers = new List<User> { new User(), new User() };
            _mockUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(allUsers);

            // Act
            var result = await _applicationServiceUser.GetAllUsers();

            // Assert
            result.Should().BeOfType<GetAllUsersResponseDTO>();
            result.Users.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUserById_WithValidId_ReturnsGetUserResponseDTO()
        {
            // Arrange
            int userId = 123;
            var user = new User();
            _mockUserService.Setup(service => service.GetUserById(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _applicationServiceUser.GetUserById(userId);

            // Assert
            result.Should().BeOfType<GetUserResponseDTO>();
        }
    }
}