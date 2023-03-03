using QuizPop.Models.Entity;

namespace QuizPop.Models.View;

/// <summary>
///     Our quiz view model.
///     This is the model that we use to pass data to our Quiz views.
///     It contains a single quiz and a list of quizzes.
/// </summary>
public class QuizViewModel
{
    /// <summary>
    ///     The quiz that we are currently viewing.
    /// </summary>
    public Quiz? Quiz { get; set; }

    /// <summary>
    ///     A list of quizzes that we can view.
    /// </summary>
    public IEnumerable<Quiz>? Quizzes { get; set; }
}