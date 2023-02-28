using QuizPop.Models.Entity;

namespace QuizPop.Models.View;

public class QuizViewModel
{
    public Quiz? Quiz { get; set; }
    public IEnumerable<Quiz>? Quizzes { get; set; }
}