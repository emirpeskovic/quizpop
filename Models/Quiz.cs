using QuizPop.DAL;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizPop.Models
{
    public class Quiz : IEntity
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [NotNull]
        public string Title { get; init; }
        
    }
}
