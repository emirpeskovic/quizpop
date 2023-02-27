using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizPop.Models;

namespace QuizPop.API
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("post")]
        [Consumes("application/json")]
        public string TestPost([FromBody] TestData testData) => JsonConvert.SerializeObject(testData);
    }
}
