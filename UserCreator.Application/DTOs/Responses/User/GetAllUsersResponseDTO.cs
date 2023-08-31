using UserCreator.Domain.DTOs.Responses.User;

namespace UserCreator.Application.DTOs.Responses.User;

public class GetAllUsersResponseDTO : ApiBaseResponse
{
    public List<UserResponseDTO> Users { get; set; }
}

