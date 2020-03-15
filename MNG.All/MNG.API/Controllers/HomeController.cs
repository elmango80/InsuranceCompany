using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MNG.API.Code.Contracts;
using MNG.API.Models;
using MNG.Application.Contracts;
using MNG.Infrastructure.Extensions;

using Newtonsoft.Json;

using Omu.ValueInjecter;

namespace MNG.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly IClientService _clientService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITokenManager tokenManager, IClientService clientService, ILogger<HomeController> logger)
        {
            _tokenManager = tokenManager;
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Status()
        {
            return Ok(new { status = "Server OK!" });
        }

        [HttpPost]
        public IActionResult Token([FromBody] User userLogin)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userLogged = _clientService.GetClientByName(userLogin.Name);

                if (!userLogged.IsValid)
                {
                    return NotFound(JsonConvert.SerializeObject(new { error = userLogged.Message }));
                }

                var currentUser = (User)new User().InjectFrom(userLogged.Model);
                var tokenJWR = _tokenManager.GetJWT(currentUser);

                return Ok(JsonConvert.SerializeObject(tokenJWR));
            }
            catch (System.Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { error = ex.HandlerException(_logger) }));
            }
        }
    }
}
