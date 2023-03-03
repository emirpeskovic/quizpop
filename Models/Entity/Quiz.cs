using System.ComponentModel.DataAnnotations;
using QuizPop.DAL.Common;

namespace QuizPop.Models.Entity;

/// <summary>
///     Our quiz entity
/// </summary>
public class Quiz : BaseEntity
{
    /// <summary>
    ///     The title of the quiz
    /// </summary>
    [Required]
    public string Title { get; init; }
}