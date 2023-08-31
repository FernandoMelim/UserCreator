using UserCreator.Domain.Entities;

namespace UserCreator.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateUser(User user);

    Task EditUser(User user);

    Task DeleteUser(int id);

    Task<User> GetUser(int id);

    Task<IEnumerable<User>> GetAllUsers();
}

