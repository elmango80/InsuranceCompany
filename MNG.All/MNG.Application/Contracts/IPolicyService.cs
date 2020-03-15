using MNG.Application.DTOs;
using MNG.Infrastructure.Models;

namespace MNG.Application.Contracts
{
    public interface IPolicyService
    {
        ModelResponse<PolicyDTO> GetPolicyById(string id);

        ModelsResponse<PolicyDTO> GetPoliciesLinkedByIdClient(string idClient);
    }
}