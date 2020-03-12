using System.Linq;

using MNG.Domain.Entities;
using MNG.Domain.Values;
using MNG.Infrastructure.Contracts;
using MNG.Infrastructure.Models;
using MNG.Infrastructure.Helpers;

using Omu.ValueInjecter;

namespace MNG.Infrastructure.Repositories
{
    public class PoliciesRespository : IRepository<Policy>
    {
        public ModelsResponse<Policy> GetData()
        {
            var result = new ModelsResponse<Policy>();
            var clients = ResourcesHandler.Load<Policy>(AddressValues.POLICIES, TableNameValues.POLICIES);

            if (!clients.IsValid)
            {
                result.NotValidResponse(clients.Message);

                return result;
            }

            result.IsValid = true;
            result.Models = clients.Models.Select(c => (Policy)new Policy().InjectFrom(c));

            return result;
        }
    }
}