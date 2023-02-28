using System.ComponentModel.DataAnnotations;
using QuizPop.DAL;

namespace QuizPop.Models;

public class Quiz : IEntity
{
    [Required] public string Title { get; init; }
    [Key] public int Id { get; init; }
}