using MNG.Application.DTOs;
using MNG.Infrastructure.Models;

namespace MNG.Application.Contracts
{
    public interface IPolicyService
    {
        /// <summary>
        /// Get policy information searching by polity ID.
        /// </summary>        
        /// <param id="79c689f3-053a-459b-8c88-32a699817097"></param>
        /// <returns></returns>
        ResponseModel<PolicyDTO> GetPolicyById(string id);

        /// <summary>
        /// Get policies linked to client by client ID.
        /// </summary>        
        /// <param id="a74c83c5-e271-4ecf-a429-d47af952cfd4"></param>
        /// <returns></returns>
        ResponseModels<PolicyDTO> GetPoliciesLinkedByIdClient(string idClient);
    }
}