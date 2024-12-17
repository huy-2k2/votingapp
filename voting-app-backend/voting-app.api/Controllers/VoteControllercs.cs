using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.share.Contract;

namespace voting_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private IVoteService _voteService;
        private IConnectionManager _connectionManager;
        private IContextService _contextService;

        public VoteController(IVoteService voteService, IConnectionManager connectionManager, IContextService contextService)
        {
            this._voteService = voteService;
            this._connectionManager = connectionManager;
            this._contextService = contextService;
        }


        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] VoteDto dto)
        {

            var result = await _voteService.CreateAsync(dto);
            _connectionManager.DisposeConnection();
            return Ok(result);
        }

        [HttpGet("entire")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEntireAsync([FromQuery] Guid id)
        {

            var result = await _voteService.GetEntireByByIdAsync(id);
            _connectionManager.DisposeConnection();
            return Ok(result);
        }


        [HttpPut("voteStatus")]
        public async Task<IActionResult> UpdateStatusAsync([FromQuery] Guid voteID, [FromQuery] int status)
        {
            await _voteService.UpdateStatusAsync(status, voteID);
            _connectionManager.DisposeConnection();
            return Ok();
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetAllByUserIdAsync()
        {
            var result = await _voteService.GetAllByUserId();
            _connectionManager.DisposeConnection();
            return Ok(result);
        }

    }
}
