using FluentValidation;
using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Application.DtoValidators;

public class PatchUserRequestValidator : AbstractValidator<PatchUserRequestDTO>
{
    private string _phonePattern = @"^\+?[0-9]*$";

    public PatchUserRequestValidator()
    {
        RuleFor(dto => dto.Id)
            .NotEmpty()
            .WithMessage("O id do usuario deve estar preenchido para a alteração ocorrer.");

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("O nome deve estar preenchido.")
            .Length(3, 255)
            .WithMessage("O nome deve ter no minimo 3 e no máximo 255 caracteres.");

        RuleFor(dto => dto.Email)
            .NotEmpty()
            .WithMessage("O e-mail deve estar preenchido.")
            .EmailAddress()
            .WithMessage("O e-mail fornecido não é válido.");

        RuleFor(dto => dto.BirthDate)
            .NotEmpty()
            .WithMessage("A data de nascimento deve estar preenchida.")
            .Must(BeAValidDate)
            .WithMessage("A data de nascimento não é válida.")
            .Must(BeInPast).WithMessage("A data de nascimento deve ser no passado.");

        RuleFor(dto => dto.SchoolingLevel)
            .NotEmpty()
            .WithMessage("A escolaridade deve estar preenchida.")
            .IsInEnum()
            .WithMessage("A escolaridade especificada não é válida.");

        RuleFor(dto => dto.Phone)
            .NotEmpty()
            .WithMessage("O telefone deve estar preenchido..")
            .Matches(_phonePattern)
            .WithMessage("O telefone fornecido não é válido.");

        RuleFor(dto => dto.Adresses)
            .NotEmpty()
            .WithMessage("O usuário deve ter um endereço preenchido.")
            .ForEach(address => address.SetValidator(new ChangeAddressRequestValidator()));
    }


    private bool BeAValidDate(DateTime? date)
        => date is DateTime;
    

    private bool BeInPast(DateTime? date)
        => date <= DateTime.Now;
}

public class ChangeAddressRequestValidator : AbstractValidator<ChangeAddressRequestDTO>
{
    public ChangeAddressRequestValidator()
    {
        RuleFor(address => address.Id)
            .GreaterThan(0).WithMessage("O id do endereço deve estar preenchido para a alteração ocorrer.");

        RuleFor(address => address.Street)
            .NotEmpty().WithMessage("O campo rua deve estar preenchido.")
            .Length(3, 255).WithMessage("O campo rua deve ter entre 3 e 255 caracteres.");

        RuleFor(address => address.Number)
            .GreaterThan(0).WithMessage("O campo número é obrigatório.");

        RuleFor(address => address.City)
            .NotEmpty().WithMessage("O campo cidade deve estar preenchido.")
            .Length(3, 255).WithMessage("O campo cidade deve ter entre 3 e 255 caracteres.");

        RuleFor(address => address.State)
            .NotEmpty().WithMessage("O campo estado deve estar preenchido.")
            .Length(3, 255).WithMessage("O campo estado deve ter entre 3 e 255 caracteres.");

        RuleFor(address => address.PostalCode)
            .NotEmpty().WithMessage("O campo CEP deve estar preenchido.")
            .Length(9).WithMessage("O CEP deve ter obrigatoriamente 9 caracteres.");
    }
}

