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
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IConnectionManager _connectionManager;
        private IContextService _contextService;


        public AuthController(IAuthService authService, IConnectionManager connectionManager, IContextService contextService)
        {
            this._authService = authService;
            this._connectionManager = connectionManager;
            this._contextService = contextService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromBody] String token)
        {
            var tokenResult = await this._authService.LoginWithGoogle(token);
            _connectionManager.DisposeConnection();

            return Ok(tokenResult);
        }

        [HttpGet("")]
        public IActionResult CheckToken()
        {
            return Ok("signed"); 
        }

    }
}
