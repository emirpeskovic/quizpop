using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizPop.Models.Entity;
using QuizPop.Services;

namespace QuizPop.API;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly QuizService _quizService;

    public QuizController(QuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet]
    public IActionResult GetQuiz()
    {
        return Ok(JsonConvert.SerializeObject(_quizService.GetQuiz() ?? new Quiz()));
    }

    [HttpPost]
    public IActionResult CreateQuiz([FromBody] Quiz quiz)
    {
        return Ok(JsonConvert.SerializeObject(_quizService.CreateQuiz(quiz)));
    }
}