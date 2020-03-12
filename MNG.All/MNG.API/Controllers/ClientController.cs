using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MNG.Application.Contracts;
using MNG.Infrastructure.Extensions;
using MNG.Infrastructure.Models;
using Newtonsoft.Json;

namespace MNG.API.Controllers
{
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
        public string GetById(string id)
        {
            try
            {
                var response =  _clientService.GetClientById(id);

                return JsonConvert.SerializeObject(response);
            }
            catch (System.Exception ex)
            {
                return JsonConvert.SerializeObject(ex.HandlerException(_logger));
            }
        }

        [HttpGet("{name}")]
        [ActionName("get-by-name")]
        public string GetByName(string name)
        {
            try
            {
                var response = _clientService.GetClientByName(name);

                return JsonConvert.SerializeObject(response);
            }
            catch (System.Exception ex)
            {
                return JsonConvert.SerializeObject(ex.HandlerException(_logger));
            }
        }

        [HttpGet("{name}")]
        [ActionName("get-policies-linked-by-name")]
        public string GetPoliciesLinkedByName(string name)
        {
            try
            {
                var response = _clientService.GetPoliciesLinkedByName(name);

                return JsonConvert.SerializeObject(response);
            }
            catch (System.Exception ex)
            {
                return JsonConvert.SerializeObject(ex.HandlerException(_logger));
            }
        }

        [HttpGet("{idPolity}")]
        [ActionName("get-by-policy-id")]
        public string GetClientByIdPolicy(string idPolity)
        {
            try
            {
                var response = _clientService.GetClientByIdPolicy(idPolity);

                return JsonConvert.SerializeObject(response);
            }
            catch (System.Exception ex)
            {
                return JsonConvert.SerializeObject(ex.HandlerException(_logger));
            }
        }
    }
}
