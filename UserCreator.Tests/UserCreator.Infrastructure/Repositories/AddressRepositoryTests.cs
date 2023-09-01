using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Entities;
using UserCreator.Infrastructure.AppContext;
using UserCreator.Infrastructure.Repositories;

namespace UserCreator.Tests.UserCreator.Infrastructure.Repositories;

public class AddressRepositoryTests
{
    private async Task SeedData(ApplicationContext dbContext)
    {
        var addresses = new[]
        {
            new Address { PostalCode = "12345", City = "test", State = "Uf", Street = "test" },
            new Address { PostalCode = "67890", City = "test", State = "Uf", Street = "test" }
        };

        await dbContext.Addresses.AddRangeAsync(addresses);
        await dbContext.SaveChangesAsync();
    }

    private ApplicationContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }

    [Fact]
    public async Task PostalCodeExistsInDatabase_PostalCodeExists_ReturnsTrue()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        await SeedData(dbContext);

        var repository = new AddressRepository(dbContext);

        // Act
        var exists = await repository.PostalCodeExistsInDatabase("12345");

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task PostalCodeExistsInDatabase_PostalCodeDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext();
        await SeedData(dbContext);

        var repository = new AddressRepository(dbContext);

        // Act
        var exists = await repository.PostalCodeExistsInDatabase("99999");

        // Assert
        Assert.False(exists);
    }
}

