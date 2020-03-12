using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MNG.Application.Contracts;
using MNG.Infrastructure.Extensions;

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

        /// <summary>
        /// Get client information searching by your ID.
        /// </summary>        
        /// <param id="a74c83c5-e271-4ecf-a429-d47af952cfd4"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get client information searching by your name.
        /// </summary>        
        /// <param id="Barnett"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get policies linked to client searching by your name.
        /// </summary>        
        /// <param id="Barnett"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get client information searching by polity ID.
        /// </summary>        
        /// <param id="79c689f3-053a-459b-8c88-32a699817097"></param>
        /// <returns></returns>
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
