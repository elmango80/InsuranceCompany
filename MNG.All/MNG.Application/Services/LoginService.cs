using MNG.Application.Contracts;
using MNG.Application.DTOs;
using MNG.Infrastructure.Helpers;
using MNG.Infrastructure.Models;

namespace MNG.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IClientService _clientService;

        public LoginService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public ModelResponse<ClientDTO> LoginUser(string name)
        {
            var result = new ModelResponse<ClientDTO>();
            var user = _clientService.GetClientByName(name);

            if (!user.IsValid)
            {
                result.NotValidResponse(user.Message);

                return result;
            }

            return result;
        }
    }
}
