using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Entities;

namespace UserCreator.Infrastructure.AppContext.EntitiesMappers;

public static class UserEntityMapper
{
    public static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");

        modelBuilder.Entity<User>(x =>
        {
            x.ToTable("Users");
            x.HasKey(c => c.Id).HasName("Id");
            x.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            x.Property(c => c.Name).HasColumnName("Name").HasMaxLength(255);
            x.Property(c => c.Email).HasColumnName("Email").HasMaxLength(255);
            x.Property(c => c.BirthDate).HasColumnName("BirthDate");
            x.Property(c => c.SchoolingLevel).HasColumnName("SchoolingLevel");
            x.Property(c => c.Phone).HasColumnName("Phone").HasMaxLength(255);

            x.HasMany(u => u.Adresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        });
    }
}

