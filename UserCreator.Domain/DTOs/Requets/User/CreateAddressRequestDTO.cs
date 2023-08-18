using System.ComponentModel.DataAnnotations;

namespace UserCreator.Domain.DTOs.Requets.User;

public class CreateAddressRequestDTO
{
    [Required(ErrorMessage = "O campo 'Rua' é obrigatório")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "A rua deve ter no minimo 3 e no máximo 255 caracteres.")]
    [DataType(DataType.Text)]
    public string Street { get; set; }

    [Required(ErrorMessage = "O campo 'Número' é obrigatório")]
    public int Number { get; set; }

    [Required(ErrorMessage = "O campo 'Cidade' é obrigatório")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "A cidade deve ter no minimo 3 e no máximo 255 caracteres.")]
    [DataType(DataType.Text)]
    public string City { get; set; }

    [Required(ErrorMessage = "O campo 'Estado' é obrigatório")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "O estado deve ter no minimo 3 e no máximo 255 caracteres.")]
    [DataType(DataType.Text)]
    public string State { get; set; }

    [Required(ErrorMessage = "O campo 'CEP' é obrigatório")]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve ter obrigatóriamente 9 caracteres.")]
    [DataType(DataType.Text)]
    public string PostalCode { get; set; }
}

