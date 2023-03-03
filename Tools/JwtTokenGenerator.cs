using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QuizPop.Models.Entity;

namespace QuizPop.Tools;

public static class JwtTokenGenerator
{
    private static string GenerateJwtToken()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }

    public static string GenerateToken(User user)
    {
        // Create the handler
        var handler = new JwtSecurityTokenHandler();

        // Get the bytes of our key
        var key = Encoding.ASCII.GetBytes(GenerateJwtToken());

        // Create the descriptor
        var descriptor = new SecurityTokenDescriptor
        {
            // Our subject will be the email of the user
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Now we need to sign the token
        var token = handler.CreateToken(descriptor);

        // Finally, we serialize the token
        var tokenString = handler.WriteToken(token);

        return tokenString;
    }
}