using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PracticeBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }
        [HttpGet]
        [Route("")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError(Activity.Current?.Id, "Server error");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
