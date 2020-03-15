using MNG.Application.DTOs;
using MNG.Infrastructure.Models;

namespace MNG.Application.Contracts
{
    public interface IClientService
    {
        /// <summary>
        /// Get client information searching by your ID.
        /// </summary>        
        /// <param id="a74c83c5-e271-4ecf-a429-d47af952cfd4"></param>
        /// <returns>
        /// </returns>
        ResponseModel<ClientDTO> GetClientById(string id);

        /// <summary>
        /// Get client information searching by your name.
        /// </summary>        
        /// <param id="Barnett"></param>
        /// <returns></returns>
        ResponseModel<ClientDTO> GetClientByName(string name);

        /// <summary>
        /// Get policies linked to client searching by your name.
        /// </summary>        
        /// <param id="Barnett"></param>
        /// <returns></returns>
        ResponseModels<PolicyDTO> GetPoliciesLinkedByName(string name);

        /// <summary>
        /// Get client information searching by polity ID.
        /// </summary>        
        /// <param id="79c689f3-053a-459b-8c88-32a699817097"></param>
        /// <returns></returns>
        ResponseModel<ClientDTO> GetClientByIdPolicy(string idPolicy);
    }
}