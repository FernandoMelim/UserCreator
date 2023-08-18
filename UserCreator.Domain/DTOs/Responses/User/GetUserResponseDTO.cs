namespace UserCreator.Domain.DTOs.Responses.User;

public class GetUserResponseDTO : ApiBaseResponse
{
    public Entities.User User { get; set; }
}

