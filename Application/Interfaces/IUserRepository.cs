using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    public User GetUserByEmail(string Email);

    public User CreateNewUser(User user);
    
}