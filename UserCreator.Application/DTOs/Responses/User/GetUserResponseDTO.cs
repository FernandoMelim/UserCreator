using UserCreator.Domain.DTOs.Responses.User;

namespace UserCreator.Application.DTOs.Responses.User;

public class GetUserResponseDTO : ApiBaseResponse
{
    public UserResponseDTO User { get; set; }
}

