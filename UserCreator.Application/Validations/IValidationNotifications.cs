﻿namespace UserCreator.Application.Validations;

public interface IValidationNotifications
{
    void AddError(string key, string error);

    bool HasErrors();

    IDictionary<string, List<string>> GetErrors();
}

