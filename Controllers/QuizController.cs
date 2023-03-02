using Microsoft.AspNetCore.Mvc;
using QuizPop.Models.View;
using QuizPop.Services;

namespace QuizPop.Controllers;

public class QuizController : Controller
{
    private readonly QuizService _quizService;

    public QuizController(QuizService quizService)
    {
        _quizService = quizService;
    }

    public IActionResult Index([FromQuery(Name = "quiz-id")] int quizId = 0, int page = 0, int count = 12)
    {
        if (quizId == 0)
            return View(new QuizViewModel
            {
                Quiz = null,
                Quizzes = _quizService.GetQuizzes(page, count)
            });

        var quiz = _quizService.GetQuiz(q => q.Id == quizId);
        return PartialView(new QuizViewModel
        {
            Quiz = quiz,
            Quizzes = null
        });
    }
}