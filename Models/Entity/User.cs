using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using QuizPop.DAL;

namespace QuizPop.Models.Entity;

public class User : IEntity
{
    /// <summary>
    ///     The ID of the user.
    /// </summary>
    [Key]
    public int Id { get; init; }
    
    /// <summary>
    ///     The email of the user.
    /// </summary>
    [Required]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    /// <summary>
    ///     The password hash of the user.
    /// </summary>
    [Required]
    public byte[] PasswordHash { get; set; }
    
    /// <summary>
    ///     The salt of the user.
    /// </summary>
    [Required]
    public byte[] Salt { get; set; }
    
    /// <summary>
    ///     The role of the user.
    /// </summary>
    [DefaultValue("Member")]
    public string Role { get; set; }
}