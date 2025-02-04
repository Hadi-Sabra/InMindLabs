using Lab1.Models;

namespace Lab1.Services;

public interface IUserService
{
    List<User> GetAllUsers();
    User GetUserById(long id);
    List<User> GetUsersByName(string name);
    User UpdateUser(UpdateUserRequest request);
    bool DeleteUser(long id);
}