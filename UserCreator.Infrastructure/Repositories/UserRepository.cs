using Microsoft.EntityFrameworkCore;
using UserCreator.Domain.Entities;
using UserCreator.Domain.Interfaces.Repositories;
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

    public async Task CreateUser(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task EditUser(User user)
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

        await _db.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = _db.Users.Include(x => x.Adresses).FirstOrDefault(x => x.Id == id);

        if (user == null)
            throw new ObjectNotFoundException();

        _db.Addresses.RemoveRange(user.Adresses);
        _db.Users.Remove(user);

        await _db.SaveChangesAsync();
    }

    public async Task<User> GetUser(int id)
    {
        var user = await _db.Users.Include(x => x.Adresses).FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new ObjectNotFoundException();

        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
        => await _db.Users.Include(x => x.Adresses).ToListAsync();
}

