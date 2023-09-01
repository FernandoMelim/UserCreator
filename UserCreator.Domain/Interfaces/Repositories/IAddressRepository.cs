namespace UserCreator.Domain.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<bool> PostalCodeExistsInDatabase(string postalCode);
}

