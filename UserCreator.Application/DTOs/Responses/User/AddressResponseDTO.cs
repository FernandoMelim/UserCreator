namespace UserCreator.Application.DTOs.Responses.User;

public class AddressResponseDTO
{
    public int Id { get; set; }

    public int Number { get; set; }
    public int UserId { get; set; }

    public string Street { get; set; }


    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }
}

