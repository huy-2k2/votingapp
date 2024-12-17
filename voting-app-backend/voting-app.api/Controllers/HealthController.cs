using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using voting_app.application.Contract;
using voting_app.infrastructure;
using voting_app.share.Constant;
using voting_app.share.Contract;

namespace voting_app.api.Controllers
{
    [Route("")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        public HealthController(IAuthService authService, IConnectionManager connectionManager, IContextService contextService)
        {
            
        }

        [HttpGet("health/ready")]
        [AllowAnonymous]
        public async Task<IActionResult> Health()
        {
            return Ok("hello world");
        }
    }
}
