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
        // For some reason, quiz is not null when it should be.
        // This solves the problem, checking if the Id is 0.
        if (quiz != null && quiz.Id != 0)
            return PartialView(new QuizViewModel
            {
                Quiz = quiz,
                Quizzes = null
            });

        return View(new QuizViewModel
        {
            Quiz = null,
            Quizzes = quiz?.Id == 0 || quiz == null ? _quizService.GetQuizzes(page, count) : null
        });
    }

    [HttpGet("quiz/getQuiz")]
    public IActionResult GetQuiz([FromQuery(Name = "quiz-id")] int quizId)
    {
        var quiz = _quizService.GetQuiz(q => q.Id == quizId);
        return PartialView("Index", new QuizViewModel
        {
            Quiz = quiz,
            Quizzes = null
        });
    }
}