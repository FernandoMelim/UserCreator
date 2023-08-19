using System.ComponentModel.DataAnnotations;

namespace UserCreator.Domain.DTOs.Requets.User;

public class CreateAddressRequestDTO : ApiBaseRequest
{
    public string Street { get; set; }

    public int Number { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }
}

