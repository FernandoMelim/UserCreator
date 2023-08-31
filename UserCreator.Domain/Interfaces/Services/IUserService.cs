using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Interfaces.Services;

public interface IUserService
{
    Task CreateUser(User user);
    Task DeleteUser(int id);
    Task EditUser(User patchUserRequestDto);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
}

