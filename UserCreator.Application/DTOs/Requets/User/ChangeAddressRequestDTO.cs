namespace UserCreator.Application.DTOs.Requets.User;

public class ChangeAddressRequestDTO : ApiBaseRequest
{
    public int Id { get; set; }

    public string Street { get; set; }

    public int Number { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }
}

