using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using voting_app.application.Contract;
using voting_app.application.DTO;

namespace voting_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost("multi")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMultiAsync([FromBody]List<AnswerDto> answers)
        {
            await _answerService.CreateMultiAsync(answers);
            return Ok();
        }
    }
}
