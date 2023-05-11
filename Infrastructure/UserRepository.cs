using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserRepository: IUserRepository
{
    private readonly DatabaseContext _dbContext;
    
    public UserRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetUserByEmail(string Email)
    {
        return _dbContext.UserTable.FirstOrDefault(u => u.Email == Email) ??
               throw new KeyNotFoundException("There was no user with email" + Email);
    }

    public User CreateNewUser(User user)
    {
        _dbContext.UserTable.Add(user);
        _dbContext.SaveChanges();
        return user;
    }
}