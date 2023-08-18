using UserCreator.Domain.Entities;

namespace UserCreator.Domain.DTOs.Responses.User;

public class UserResponseDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    public int SchoolingLevel { get; set; }

    public string Phone { get; set; }

    public List<AddressResponseDTO> Adresses { get; set; }
}

