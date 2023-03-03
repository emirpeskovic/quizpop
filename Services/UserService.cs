using System.Text;
using Konscious.Security.Cryptography;
using QuizPop.DAL;
using QuizPop.Models.DTO;
using QuizPop.Models.Entity;

namespace QuizPop.Services;

public class UserService
{
    /// <summary>
    ///     Singleton instance of the DatabaseManager.
    /// </summary>
    private readonly DatabaseManager _databaseManager;

    /// <summary>
    ///     Constructor for the UserService.
    /// </summary>
    /// <param name="databaseManager">Our DatabaseManager singleton, automatically passed using dependency injection</param>
    public UserService(DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    /// <summary>
    ///     Returns a user if the email and password match.
    /// </summary>
    /// <param name="email">The email to find</param>
    /// <param name="hashedPassword">The argon2 hashed password bytes</param>
    /// <param name="salt">The salt bytes</param>
    /// <returns>The user or null, if the user doesn't exist or the values don't match</returns>
    public User? GetUser(string email, byte[] hashedPassword, byte[] salt)
    {
        // We use the DatabaseManager to get a user by email and password
        return _databaseManager.GetOne<User>(u => u.Email == email && u.PasswordHash == hashedPassword && u.Salt == salt);
    }

    /// <summary>
    ///     Uses the underlying GetUser method to find a user by email and password from a LoginRequest.
    /// </summary>
    /// <param name="loginRequest">The LoginRequest DTO</param>
    /// <returns>The user or null, if the user doesn't exist or the values don't match</returns>
    public User? GetUser(LoginRequest loginRequest)
    {
        // We create a new salt and use it to hash the password
        var salt = new byte[16];

        // We use 10,000 iterations for the hashing
        const int iterations = 10000;

        // We use the Argon2id hashing algorithm
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(loginRequest.Password))
        {
            Salt = salt,
            Iterations = iterations
        };

        // We return the user if it exists and the values match
        return GetUser(loginRequest.Email, argon2.GetBytes(32), salt);
    }
}