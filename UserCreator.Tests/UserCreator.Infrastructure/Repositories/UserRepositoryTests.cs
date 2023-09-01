using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Entities;
using UserCreator.Infrastructure.AppContext;
using UserCreator.Infrastructure.Exceptions;
using UserCreator.Infrastructure.Repositories;

namespace UserCreator.Tests.UserCreator.Infrastructure.Repositories;

public class UserRepositoryTests
{
    private readonly Mock<ApplicationContext> _mockDbContext;

    public UserRepositoryTests()
    {

    }

    private ApplicationContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }

    private async Task SeedData(ApplicationContext dbContext)
    {
        var users = new[]
        {
                new User { Name = "ExistingUser", Email = "test", Phone = "1234" },
                new User { Name = "AnotherUser", Email = "test", Phone = "1234" }
            };

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateUser_ValidUser_UserAdded()
    {
        // Arrange
        using var dbContext = CreateInMemoryDbContext();
        var userRepository = new UserRepository(dbContext);
        var user = new User()
        {
            BirthDate = DateTime.UtcNow,
            Email = "test",
            Name = "Test",
            Phone = "123123",
            SchoolingLevel = 2,
            Adresses = new List<Address>() {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "234234",
                    State = "SP",
                    Street = "street",
                }
            }
        }; ;

        // Act
        await userRepository.CreateUser(user);

        // Assert
        Assert.Contains(user, dbContext.Users);
    }

    [Fact]
    public async Task EditUser_ExistingUser_UserEdited()
    {
        // Arrange
        var previousEmail = "teste@email.com";
        using var dbContext = CreateInMemoryDbContext();
        var userRepository = new UserRepository(dbContext);
        var existingUser = new User()
        {
            BirthDate = DateTime.UtcNow,
            Email = previousEmail,
            Name = "Test",
            Phone = "123123",
            SchoolingLevel = 2,
            Adresses = new List<Address>() {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "234234",
                    State = "SP",
                    Street = "street",
                }
            }
        };
        dbContext.Users.Add(existingUser);
        dbContext.SaveChanges();

        var updatedUser = new User()
        {
            BirthDate = DateTime.UtcNow,
            Email = "teste_changed@teste.com",
            Name = "Test",
            Phone = "123123",
            SchoolingLevel = 2,
            Adresses = new List<Address>() {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "234234",
                    State = "SP",
                    Street = "street",
                }
            }
        };

        updatedUser.Id = existingUser.Id;

        // Act
        await userRepository.EditUser(updatedUser);

        // Assert
        var editedUser = dbContext.Users.FirstOrDefault(u => u.Id == existingUser.Id);
        Assert.NotNull(editedUser);
        Assert.Equal(updatedUser.Name, editedUser.Name);
        Assert.Equal(updatedUser.Phone, editedUser.Phone);
        Assert.NotEqual(updatedUser.Email, previousEmail);
    }

    [Fact]
    public async Task DeleteUser_ExistingUser_UserDeleted()
    {
        // Arrange
        using var dbContext = CreateInMemoryDbContext();
        var userRepository = new UserRepository(dbContext);
        var userToDelete = new User()
        {
            BirthDate = DateTime.UtcNow,
            Email = "test@test.co",
            Name = "Test",
            Phone = "123123",
            SchoolingLevel = 2,
            Adresses = new List<Address>() {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "234234",
                    State = "SP",
                    Street = "street",
                }
            }
        };

        dbContext.Users.Add(userToDelete);
        await dbContext.SaveChangesAsync();

        // Act
        await userRepository.DeleteUser(userToDelete.Id);

        // Assert
        var deletedUser = dbContext.Users.FirstOrDefault(u => u.Id == userToDelete.Id);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task GetUser_ExistingUser_ReturnsUser()
    {
        // Arrange
        using var dbContext = CreateInMemoryDbContext();
        var userRepository = new UserRepository(dbContext);
        var user = new User()
        {
            BirthDate = DateTime.UtcNow,
            Email = "test@test.co",
            Name = "Test",
            Phone = "123123",
            SchoolingLevel = 2,
            Adresses = new List<Address>() {
                new Address()
                {
                    City = "city",
                    Number = 1,
                    PostalCode = "234234",
                    State = "SP",
                    Street = "street",
                }
            }
        };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        // Act
        var retrievedUser = await userRepository.GetUser(user.Id);

        // Assert
        Assert.Equal(user.Id, retrievedUser.Id);
    }

    [Fact]
    public async Task GetUser_NonExistingUser_ThrowsException()
    {
        // Arrange
        using var dbContext = CreateInMemoryDbContext();
        var userRepository = new UserRepository(dbContext);

        // Act and Assert
        await Assert.ThrowsAsync<ObjectNotFoundException>(() => userRepository.GetUser(123));
    }

    [Fact]
    public async Task GetAllUsers_ReturnsAllUsers()
    {
        // Arrange
        using var dbContext = CreateInMemoryDbContext();
        var userRepository = new UserRepository(dbContext);
        var users = new List<User>
            {
                new User()
                {
                    BirthDate = DateTime.UtcNow,
                    Email = "test@test.co",
                    Name = "Test",
                    Phone = "123123",
                    SchoolingLevel = 2,
                    Adresses = new List<Address>() {
                        new Address()
                        {
                            City = "city",
                            Number = 1,
                            PostalCode = "234234",
                            State = "SP",
                            Street = "street",
                        }
                    }
                },
                new User() { BirthDate = DateTime.UtcNow, Email = "test@test.co", Name = "Test", Phone = "123123", SchoolingLevel = 2, Adresses = new List < Address >() { new Address() { City = "city", Number = 1, PostalCode = "234234", State = "SP", Street = "street" } } },
                new User()                     
                {
                    BirthDate = DateTime.UtcNow,
                    Email = "test@test.co",
                    Name = "Test",
                    Phone = "123123",
                    SchoolingLevel = 2,
                    Adresses = new List<Address>() {
                        new Address()
                        {
                            City = "city",
                            Number = 1,
                            PostalCode = "234234",
                            State = "SP",
                            Street = "street",
                        }
                    }
                },
            };
        dbContext.Users.AddRange(users);
        await dbContext.SaveChangesAsync();

        // Act
        var retrievedUsers = await userRepository.GetAllUsers();

        // Assert
        Assert.Equal(users.Count, retrievedUsers.Count());
    }

    [Fact]
    public async Task UserExistsInDatabase_UserExists_ReturnsTrue()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        await SeedData(dbContext);

        var repository = new UserRepository(dbContext);

        // Act
        var exists = await repository.UserExistsInDatabase("ExistingUser");

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task UserExistsInDatabase_UserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        await SeedData(dbContext);

        var repository = new UserRepository(dbContext);

        // Act
        var exists = await repository.UserExistsInDatabase("NonExistingUser");

        // Assert
        Assert.False(exists);
    }
}

