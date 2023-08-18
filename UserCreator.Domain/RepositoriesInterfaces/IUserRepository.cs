using UserCreator.Domain.Entities;

namespace UserCreator.Domain.RepositoriesInterfaces;

public interface IUserRepository
{
    User CreateUser(User user);

    User EditUser(User user);

    void DeleteUser(int id);

    User GetUser(int id);

    IEnumerable<User> GetAllUsers();
}

