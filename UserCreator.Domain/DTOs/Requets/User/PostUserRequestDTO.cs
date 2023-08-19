using System.ComponentModel.DataAnnotations;
using UserCreator.Domain.Enums;

namespace UserCreator.Domain.DTOs.Requets.User;

public class PostUserRequestDTO : ApiBaseRequest
{
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }

    public SchoolingLevelEnum? SchoolingLevel { get; set; }

    public string Phone { get; set; }

    public List<CreateAddressRequestDTO> Adresses { get; set; }
}

