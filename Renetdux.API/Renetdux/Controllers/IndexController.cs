using Microsoft.AspNetCore.Mvc;
using Renetdux.Infrastructure.Common;

namespace Renetdux.Controllers
{
    public class IndexController : Controller
    {
        private readonly Configuration _configuration;

        public IndexController(Configuration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok($"Hello world! - App ID: {_configuration.AppId}");
        }
    }
}
