using UserCreator.Application.DTOs.Requets.User;
using UserCreator.Domain.Entities;

namespace UserCreator.Application.ApplicationServicesInterfaces;

public interface IApplicationServiceUser
{
    Task CreateUser(PostUserRequestDTO postUserRequestDto);
    Task DeleteUser(int id);
    Task EditUser(PatchUserRequestDTO patchUserRequestDto);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
}

