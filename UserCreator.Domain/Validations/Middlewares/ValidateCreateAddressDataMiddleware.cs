using System.ComponentModel.DataAnnotations;
using UserCreator.Domain.DTOs.Requets;
using UserCreator.Domain.DTOs.Requets.User;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateCreateAddressDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;

    public ValidateCreateAddressDataMiddleware(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));

    }

    public void Validate(ApiBaseRequest apiBaseRequest)
    {
        var addresses = (apiBaseRequest as PostUserRequestDTO).Adresses;

        if(!addresses.Any())
        {
            _validationNotifications.AddError("Addresses", "O usuário deve ter um endereço preenchido.");
            return;
        }

        foreach(var address in addresses)
        {
            if (string.IsNullOrWhiteSpace(address.Street))
                _validationNotifications.AddError("Address.Street", "O campo rua deve estar preenchido.");

            if (address.Street.Length < 3 || address.Street.Length > 255)
                _validationNotifications.AddError("Address.Street", "O campo rua deve ter no minimo 3 e no máximo 255 caracteres.");

            if(address.Number == 0)
                _validationNotifications.AddError("Address.Number", "O campo numero é obrigatório.");

            if (string.IsNullOrWhiteSpace(address.City))
                _validationNotifications.AddError("Address.City", "O campo cidade deve estar preenchido.");

            if (address.City.Length < 3 || address.City.Length > 255)
                _validationNotifications.AddError("Address.City", "O campo cidade deve ter no minimo 3 e no máximo 255 caracteres.");

            if (string.IsNullOrWhiteSpace(address.State))
                _validationNotifications.AddError("Address.State", "O campo estado deve estar preenchido.");

            if (address.State.Length < 3 || address.State.Length > 255)
                _validationNotifications.AddError("Address.State", "O campo estado deve ter no minimo 3 e no máximo 255 caracteres.");

            if (string.IsNullOrWhiteSpace(address.PostalCode))
                _validationNotifications.AddError("Address.PostalCode", "O campo CEP deve estar preenchido.");

            if (address.PostalCode.Length != 9)
                _validationNotifications.AddError("Address.PostalCode", "O CEP deve ter obrigatóriamente 9 caracteres.");
        }
    }
}

