using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MNG.Application.Contracts;
using MNG.Infrastructure.Extensions;
using MNG.Infrastructure.Models;

using Newtonsoft.Json;

namespace MNG.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ActionName("get-by-id")]
        [Authorize(Roles = "admin, user")]
        public IActionResult GetById(string id)
        {
            try
            {
                var response =  _clientService.GetClientById(id);

                return HandlerResponse(response, response.Model);
            }
            catch (System.Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.HandlerException(_logger)));
            }
        }

        [HttpGet("{name}")]
        [ActionName("get-by-name")]
        [Authorize(Roles = "admin, user")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var response = _clientService.GetClientByName(name);

                return HandlerResponse(response, response.Model);
            }
            catch (System.Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.HandlerException(_logger)));
            }
        }

        [HttpGet("{name}")]
        [ActionName("get-policies-linked-by-name")]
        [Authorize(Roles = "admin")]
        public IActionResult GetPoliciesLinkedByName(string name)
        {
            try
            {
                var response = _clientService.GetPoliciesLinkedByName(name);

                return HandlerResponse(response, response.Models);
            }
            catch (System.Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.HandlerException(_logger)));
            }
        }

        [HttpGet("{idPolity}")]
        [ActionName("get-by-policy-id")]
        [Authorize(Roles = "admin")]
        public IActionResult GetClientLinkedByIdPolicy(string idPolity)
        {
            try
            {
                var response = _clientService.GetClientByIdPolicy(idPolity);

                return HandlerResponse(response, response.Model);
            }
            catch (System.Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.HandlerException(_logger)));
            }
        }

        private IActionResult HandlerResponse(ResponseBase response, object data)
        {
            if (!response.IsValid)
            {
                return NotFound(JsonConvert.SerializeObject(response.Message));
            }

            return Ok(JsonConvert.SerializeObject(data));
        }
    }
}
