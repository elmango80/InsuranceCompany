using MNG.Application.DTOs;
using MNG.Infrastructure.Models;

namespace MNG.Application.Contracts
{
    public interface IClientService
    {
        ModelResponse<ClientDTO> GetClientById(string id);

        ModelResponse<ClientDTO> GetClientByName(string name);

        ModelsResponse<PolicyDTO> GetPoliciesLinkedByName(string name);

        ModelResponse<ClientDTO> GetByIdPolicy(string idPolicy);
    }
}