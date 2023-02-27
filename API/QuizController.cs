using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPop.Services;

namespace QuizPop.API
{
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
        [Route("GetQuiz")]
        public IActionResult GetQuiz()
        {
            return Ok("Hello World");
        }
    }
}
