using QuizPop.DAL;
using QuizPop.Models;

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
}