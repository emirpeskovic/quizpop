using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using QuizPop.DAL;

namespace QuizPop.Models.Entity;

public class User : IEntity
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
    public string Email { get; init; }
    
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
    public string Password { get; init; }
    
    [DefaultValue("Member")]
    public string Role { get; set; }
}