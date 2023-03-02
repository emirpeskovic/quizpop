using QuizPop.DAL;
using QuizPop.Models.DTO;
using QuizPop.Models.Entity;

namespace QuizPop.Services;

public class UserService
{
    private readonly DatabaseManager _databaseManager;
    
    public UserService(DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }
    
    public User? GetUser(string email, string password)
    {
        return _databaseManager.GetOne<User>(u => u.Email == email && u.Password == password);
    }
    
    public User? GetUser(LoginRequest loginRequest)
    {
        return GetUser(loginRequest.Email, loginRequest.Password);
    }
}