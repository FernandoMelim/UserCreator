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
        modelBuilder.HasDefaultSchema("UserManager");
        
        MapEntities(modelBuilder);
    }

    private void MapEntities(ModelBuilder modelBuilder)
    {
        AddressEntityMapper.ConfigureAddressEntity(modelBuilder);
        UserEntityMapper.ConfigureUserEntity(modelBuilder);
    }


}

