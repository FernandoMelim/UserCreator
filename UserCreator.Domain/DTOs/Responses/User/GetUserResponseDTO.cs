namespace UserCreator.Domain.DTOs.Responses.User;

public class GetUserResponseDTO : ApiBaseResponse
{
    public UserResponseDTO User { get; set; }
}

