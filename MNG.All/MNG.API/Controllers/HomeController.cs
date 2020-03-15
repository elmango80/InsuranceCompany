using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MNG.API.Code.Contracts;
using MNG.API.Models;
using MNG.Application.Contracts;
using MNG.Infrastructure.Extensions;
using Newtonsoft.Json;

namespace MNG.API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly ILoginService _loginService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITokenManager tokenManager, ILoginService loginService, ILogger<HomeController> logger)
        {
            _tokenManager = tokenManager;
            _loginService = loginService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Status()
        {
            return Ok("Server OK!");
        }

        [HttpPost]
        public IActionResult Token([FromBody] string userEmail)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userLogged = _loginService.LoginUser(userEmail);

                if (!userLogged.IsValid)
                {
                    return BadRequest(JsonConvert.SerializeObject(userLogged.Message));
                }

                var currentUser = new User
                {
                    Email = userLogged.Model.Email,
                    Role = userLogged.Model.Role
                };

                var tokenJWR = _tokenManager.GetJWT(currentUser);

                return Ok(JsonConvert.SerializeObject(tokenJWR));
            }
            catch (System.Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.HandlerException(_logger)));
            }
            

            //if (result != null)
            //{
            //    var result = _tokenManager.GetJWT(result);

            //    return Ok(result);
            //}

            //return Ok(userLogged);

        }
    }
}
