using Lab1.Models;

namespace Lab1.Services;

public class UserService : IUserService
{
    
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, Name = "Hadi", Email = "hadi@example.com" },
        new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
    };
    
    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User GetUserById(long id)
    {
            var userById = _users.FirstOrDefault(u => u.Id == id);

            if (userById == null)
            {
                throw new KeyNotFoundException("User with given id not found");
            }
            return userById;
        
    }

    public List<User> GetUsersByName(string name)
    {
        var userByName = _users.Where(u => u.Name.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();

        if (userByName == null)
        {
            throw new KeyNotFoundException("User with given name not found");
        }
        return userByName;
    }

    public User UpdateUser(UpdateUserRequest request)
    {
        
        var user = _users.FirstOrDefault(u => u.Id == request.Id);
        if (user != null)
        {
            user.Name = request.NewName;
            if (request.Email != null)
            {
                user.Email = request.Email;
                
            }
            
        }
        return user;
    }
    
    public bool DeleteUser(long id)
    {
        var userToDelete = _users.FirstOrDefault(u => u.Id == id);

        if (userToDelete == null)
        {
            throw new KeyNotFoundException("No User deleted because user with given id not found.");
        }
        
        _users.Remove(userToDelete);
        return true;
        

    }
}