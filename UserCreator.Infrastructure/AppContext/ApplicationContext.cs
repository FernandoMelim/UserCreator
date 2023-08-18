using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Entities;
using UserCreator.Infrastructure.AppContext.EntitiesMappers;

namespace UserCreator.Infrastructure.AppContext;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
     : base(options)
    { }

    public DbSet<User> Users { get; set; }

    public DbSet<Address> Addresses{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();
        modelBuilder.HasDefaultSchema("UserCreator");
        
        MapEntities(modelBuilder);
    }

    private void MapEntities(ModelBuilder modelBuilder)
    {
        UserEntityMapper.ConfigureUserEntity(modelBuilder);
        AddressEntityMapper.ConfigureAddressEntity(modelBuilder);
    }


}

