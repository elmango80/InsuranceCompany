using System.Collections.Generic;
using System.Linq;

using MNG.Infrastructure.Contracts;
using MNG.Domain.Entities;

using Omu.ValueInjecter;
using MNG.Domain.Values;
using MNG.Infrastructure.Models;
using MNG.Infrastucture.Helpers;

namespace MNG.Infrastructure.Repositories
{
    public class PoliciesRespository : IRepository<Policy>
    {
        ModelsResponse<Policy> IRepository<Policy>.GetData()
        {
            var result = new ModelsResponse<Policy>();
            var clients = ResourcesHandler.Load<Client>(AddressValues.POLICIES, TableNameValues.POLICIES);

            if (!clients.IsValid)
            {
                result.NotValid(clients.Message);

                return result;
            }

            result.IsValid = true;
            result.Models = clients.Models.Select(c => (Policy)new Policy().InjectFrom(c));

            return result;
        }
    }
}