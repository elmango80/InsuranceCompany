using Microsoft.AspNetCore.Mvc;

namespace MNG.API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Status()
        {
            return Ok();
        }
    }
}
