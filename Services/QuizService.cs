using QuizPop.DAL;
using QuizPop.Models.Entity;

namespace QuizPop.Services;

public class QuizService
{
    private readonly DatabaseManager _databaseManager;

    public QuizService(DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public Quiz CreateQuiz(Quiz quiz)
    {
        _databaseManager.UseContext(context => { context.Add(quiz); });

        return quiz;
    }

    public Quiz? GetQuiz(Func<Quiz, bool>? match = null)
    {
        return _databaseManager.GetOne(match);
    }

    public IEnumerable<Quiz> GetQuizzes(int page = 0, int count = 12)
    {
        return _databaseManager.GetMany<Quiz>(page, count);
    }
}