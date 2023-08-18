namespace UserCreator.Domain.DTOs.Responses.User;

public class GetAllUsersResponseDTO : ApiBaseResponse
{
    public List<Entities.User> Users { get; set; }
}

