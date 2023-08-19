using UserCreator.Domain.DTOs.Requets.User;
using UserCreator.Domain.DTOs.Responses.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Application.ServicesInterfaces;

public interface IUserService
{
    Task CreateUser(PostUserRequestDTO postUserRequestDto);
    Task DeleteUser(int id);
    Task EditUser(PatchUserRequestDTO patchUserRequestDto);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
}

