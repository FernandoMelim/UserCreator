namespace UserCreator.Domain.DTOs.Responses.User;

public class GetAllUsersResponseDTO : ApiBaseResponse
{
    public List<UserResponseDTO> Users { get; set; }
}

