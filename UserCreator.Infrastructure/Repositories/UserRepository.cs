using UserCreator.Domain.Entities;
using UserCreator.Domain.RepositoriesInterfaces;
using UserCreator.Infrastructure.AppContext;
using UserCreator.Infrastructure.Exceptions;

namespace UserCreator.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _db;
    public UserRepository(ApplicationContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public User CreateUser(User user)
    {
        var a = _db.Users.Add(user);
        _db.SaveChanges();

        return user;
    }

    public User EditUser(User user)
    {
        var dbUser = _db.Users.FirstOrDefault(x => x.Id == user.Id);

        if (dbUser == null)
            throw new ObjectNotFoundException();

        dbUser.Name = user.Name;
        dbUser.Phone = user.Phone;
        dbUser.BirthDate = user.BirthDate;
        dbUser.SchoolingLevel = user.SchoolingLevel;
        dbUser.Email = user.Email;
        dbUser.Adresses = user.Adresses;

        _db.SaveChanges();

        return dbUser;
    }

    public void DeleteUser(int id)
    {
        var user = _db.Users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            throw new ObjectNotFoundException();

        _db.Users.Remove(user);

        _db.SaveChanges();
    }

    public User GetUser(int id)
    {
        var user = _db.Users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            throw new ObjectNotFoundException();

        return user;
    }

    public IEnumerable<User> GetAllUsers()
        => _db.Users.ToList();
}

