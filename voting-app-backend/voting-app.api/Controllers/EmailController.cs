using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using voting_app.application.Contract;
using voting_app.infrastructure;
using voting_app.share.Constant;
using voting_app.share.Contract;

namespace voting_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IConnectionManager _connectionManager;
        private IEmailService _emailService;


        public EmailController(IEmailService emailService, IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
            _emailService = emailService;
        }


       
        [HttpGet("byUser")]
        public async Task<IActionResult> GetEmailByUserAsync()
        {
            var result = await _emailService.GetByUserAsync();
            _connectionManager.DisposeConnection();
            return Ok(result);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid emailID)
        {
            var result = await _emailService.GetByIdAsync(emailID);
            _connectionManager.DisposeConnection();
            return Ok(result);
        }

    }
}
