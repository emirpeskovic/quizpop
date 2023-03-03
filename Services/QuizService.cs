using QuizPop.DAL;
using QuizPop.Models.Entity;

namespace QuizPop.Services;

/// <summary>
///     Service for Quiz.
///     This service is used to create, get, and update quizzes.
/// </summary>
public class QuizService
{
    /// <summary>
    ///     Singleton instance of the DatabaseManager.
    /// </summary>
    private readonly DatabaseManager _databaseManager;

    /// <summary>
    ///     Our constructor for the QuizService.
    /// </summary>
    /// <param name="databaseManager">Our DatabaseManager singleton, automatically passed using dependency injection</param>
    public QuizService(DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    /// <summary>
    ///     Creates a new quiz.
    /// </summary>
    /// <param name="quiz">The new quiz object</param>
    /// <returns>The same quiz object</returns>
    public Quiz CreateQuiz(Quiz quiz)
    {
        // We use the DatabaseManager to add the quiz to the database
        _databaseManager.UseContext(context => { context.Add(quiz); });

        // We return the quiz
        return quiz;
    }

    /// <summary>
    ///     Gets a quiz by id.
    /// </summary>
    /// <param name="match"></param>
    /// <returns></returns>
    public Quiz? GetQuiz(Func<Quiz, bool>? match = null)
    {
        // We use the DatabaseManager to get a quiz by id
        return _databaseManager.GetOne(match);
    }

    /// <summary>
    ///     Gets multiple quizzes from the database.
    /// </summary>
    /// <param name="page">Next page view</param>
    /// <param name="count"></param>
    /// <returns></returns>
    public IEnumerable<Quiz> GetQuizzes(int page = 0, int count = 12)
    {
        // We get multiple quizzes from the database based on the page and count parameters
        return _databaseManager.GetMany<Quiz>(page, count);
    }
}