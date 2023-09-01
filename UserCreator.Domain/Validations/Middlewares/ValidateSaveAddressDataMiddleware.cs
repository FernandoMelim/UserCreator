using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateSaveAddressDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;
    private readonly IAddressRepository _addressRepository;


    public ValidateSaveAddressDataMiddleware(
        IValidationNotifications validationNotifications,
        IAddressRepository addressRepository
        )
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
    }

    public async virtual Task Validate(BaseEntity user)
    {
        var addresses = (user as User).Adresses;

        foreach ( var address in addresses )
        {
            var userExists = await _addressRepository.PostalCodeExistsInDatabase(address.PostalCode);
            if (userExists)
                _validationNotifications.AddError($"Adsress[{address.PostalCode}]", "Já existe um endereço cadastrado com CEP.");
        }

    }
}

