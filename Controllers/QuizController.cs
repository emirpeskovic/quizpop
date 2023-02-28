using Microsoft.AspNetCore.Mvc;
using QuizPop.Models.Entity;
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

    public IActionResult Index(Quiz? quiz = null, int page = 0, int count = 12)
    {
        return View(new QuizViewModel
        {
            Quiz = quiz,
            Quizzes = quiz == null ? _quizService.GetQuizzes(page, count) : null
        });
    }
}