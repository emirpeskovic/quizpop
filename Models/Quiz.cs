using System.ComponentModel.DataAnnotations;
using QuizPop.DAL;

namespace QuizPop.Models;

public class Quiz : IEntity
{
    [Key] public int Id { get; init; }
    [Required] public string Title { get; init; }
}