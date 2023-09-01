using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Interfaces.Repositories;
using UserCreator.Infrastructure.AppContext;

namespace UserCreator.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationContext _db;

    public AddressRepository(ApplicationContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<bool> PostalCodeExistsInDatabase(string postalCode)
        => await _db.Addresses.Where(x => x.PostalCode.Equals(postalCode)).AnyAsync();
}

