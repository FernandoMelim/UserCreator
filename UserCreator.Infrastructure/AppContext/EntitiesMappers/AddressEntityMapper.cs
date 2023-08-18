using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Entities;

namespace UserCreator.Infrastructure.AppContext.EntitiesMappers;

public static class AddressEntityMapper
{
    public static void ConfigureAddressEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>().ToTable("Addresses");

        modelBuilder.Entity<Address>(x =>
        {
            x.ToTable("Addresses");
            x.HasKey(a => a.Id).HasName("Id");
            x.Property(a => a.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            x.Property(a => a.UserId).HasColumnName("UserId");
            x.Property(a => a.Street).HasColumnName("Street").HasMaxLength(255);
            x.Property(a => a.Number).HasColumnName("Number");
            x.Property(a => a.City).HasColumnName("City").HasMaxLength(255);
            x.Property(a => a.State).HasColumnName("State").HasMaxLength(255);
            x.Property(a => a.PostalCode).HasColumnName("PostalCode").HasMaxLength(9);

            x.HasOne(a => a.User)
                .WithMany(u => u.Adresses)
                .HasForeignKey(a => a.UserId);
        });
    }
}

