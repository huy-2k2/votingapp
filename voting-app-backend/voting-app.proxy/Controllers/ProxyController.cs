using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using voting_app.application.Contract;

namespace voting_app.proxy.Controllers
{
    [Route("")]
    [ApiController]
    public class ProxyController : ControllerBase
    {

        private readonly IProxyService _proxyService;
        public ProxyController(IProxyService proxyService) {
            _proxyService = proxyService;
        }

        [HttpGet("{*any}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetIndexFileAsync()
        {

            var indexFileContent = await _proxyService.GetIndexFileAsync();

            return Content(indexFileContent, "text/html");
        }
    }
}
