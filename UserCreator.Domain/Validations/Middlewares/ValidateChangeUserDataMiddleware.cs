﻿using System.Text.RegularExpressions;
using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Validations.Middlewares;

public class ValidateChangeUserDataMiddleware : IValidationMiddleware
{
    private readonly IValidationNotifications _validationNotifications;
    private string _emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private string _phonePattern = @"^\+?[0-9]*$";

    public ValidateChangeUserDataMiddleware(IValidationNotifications validationNotifications)
    {
        _validationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }

    public void Validate(BaseEntity user)
    {
        var instance = user as User;

        if(instance.Id == 0)
            _validationNotifications.AddError("Id", "O id do usuario deve estar preenchido para a alteração ocorrer.");

        if (string.IsNullOrWhiteSpace(instance.Name))
            _validationNotifications.AddError("Name", "O nome deve estar preenchido.");

        if (instance.Name.Length < 3 || instance.Name.Length > 255)
            _validationNotifications.AddError("Name", "O nome deve ter no minimo 3 e no máximo 255 caracteres.");

        if (string.IsNullOrWhiteSpace(instance.Email))
            _validationNotifications.AddError("Email", "O e-mail deve estar preenchido.");

        Regex regex = new Regex(_emailPattern);
        if (!regex.IsMatch(instance.Email))
            _validationNotifications.AddError("Email", "O e-mail fornecido não é válido.");

        if (!instance.BirthDate.HasValue)
            _validationNotifications.AddError("BirthDate", "A data de nascimento deve estar preenchida.");

        if (instance.BirthDate.HasValue && instance.BirthDate.Value.Date > DateTime.Now.Date)
            _validationNotifications.AddError("BirthDate", "Data de nascimento maior do que a data atual");

        if (!instance.SchoolingLevel.HasValue)
            _validationNotifications.AddError("SchoolingLevel", "A escolaridade deve estar preenchida.");

        if (instance.SchoolingLevel.HasValue && (instance.SchoolingLevel < 0 || (int)instance.SchoolingLevel > 3))
            _validationNotifications.AddError("SchoolingLevel", "Adicione um nível de escolaridade correto");


        if (string.IsNullOrWhiteSpace(instance.Phone))
            _validationNotifications.AddError("Phone", "O telefone deve estar preenchido.");

        regex = new Regex(_phonePattern);
        if (!regex.IsMatch(instance.Phone))
            _validationNotifications.AddError("Phone", "O telefone fornecido não é válido.");
    }
}

