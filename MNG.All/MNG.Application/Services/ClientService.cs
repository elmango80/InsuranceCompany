using System;
using System.Linq;

using MNG.Application.Contracts;
using MNG.Application.DTOs;
using MNG.Domain.Entities;
using MNG.Domain.Values;
using MNG.Infrastructure.Contracts;
using MNG.Infrastructure.Helpers;
using MNG.Infrastructure.Models;

using Omu.ValueInjecter;

namespace MNG.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientsRepository;
        private readonly IPolicyService _policiesService;

        public ClientService(IRepository<Client> clientsRepository, IPolicyService policiesService)
        {
            _clientsRepository = clientsRepository;
            _policiesService = policiesService;
        }

        public ModelResponse<ClientDTO> GetClientById(string id)
        {
            var result = new ModelResponse<ClientDTO>();

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }
            
            var clients = _clientsRepository.GetData();
            var client = clients.Models.SingleOrDefault(c => c.IdClient == id);

            if (client == null)
            {
                result.NotValidResponse(string.Format(MessageValues.CLIENT_NOT_FOUND, nameof(id), id));

                return result;
            }

            result.IsValid = true;
            result.Model = (ClientDTO)new ClientDTO().InjectFrom(client);

            return result;
        }

        public ModelResponse<ClientDTO> GetClientByIdPolicy(string idPolicy)
        {
            var result = new ModelResponse<ClientDTO>();

            if (string.IsNullOrEmpty(idPolicy)) 
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var politicyDTO = _policiesService.GetPolicyById(idPolicy);

            if (!politicyDTO.IsValid)
            {
                result.NotValidResponse(politicyDTO.Message);

                return result;
            }

            return GetClientById(politicyDTO.Model.IdClient);
        }

        public ModelResponse<ClientDTO> GetClientByName(string name)
        {
            var result = new ModelResponse<ClientDTO>();

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var clients = _clientsRepository.GetData();
            var client = clients.Models.SingleOrDefault(c => c.Name == name);

            if (client == null)
            {
                result.NotValidResponse(string.Format(MessageValues.CLIENT_NOT_FOUND, nameof(name), name));

                return result;
            }

            result.IsValid = true;
            result.Model = (ClientDTO)new ClientDTO().InjectFrom(client);

            return result;
        }

        public ModelsResponse<PolicyDTO> GetPoliciesLinkedByName(string name)
        {
            var result = new ModelsResponse<PolicyDTO>();

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }
            
            var clientDTO = GetClientByName(name);

            if (!clientDTO.IsValid)
            {
                result.NotValidResponse(clientDTO.Message);

                return result;
            }

            return _policiesService.GetPoliciesLinkedByIdClient(clientDTO.Model.IdClient);
        }
    }
}
