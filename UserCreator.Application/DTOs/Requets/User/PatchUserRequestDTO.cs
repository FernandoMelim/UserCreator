using System.ComponentModel.DataAnnotations;
using UserCreator.Domain.Enums;

namespace UserCreator.Application.DTOs.Requets.User;

public class PatchUserRequestDTO : ApiBaseRequest
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }

    public SchoolingLevelEnum? SchoolingLevel { get; set; }

    public string Phone { get; set; }

    public List<ChangeAddressRequestDTO> Adresses { get; set; }

}

