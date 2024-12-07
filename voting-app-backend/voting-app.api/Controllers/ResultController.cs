using Microsoft.AspNetCore.Mvc;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.share.Contract;


namespace voting_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        private readonly IConnectionManager _connectionManager;

        public ResultController(IResultService resultService, IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
            _resultService = resultService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] ResultDto dto)
        {
            var result = await _resultService.CreateAsync(dto);
            _connectionManager.DisposeConnection();
            return Ok(result);

        }
    }
}
