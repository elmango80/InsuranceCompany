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
        public ResponseModels<Policy> GetData()
        {
            var result = new ResponseModels<Policy>();
            var policies = ResourcesHandler.Load<Policy>(AddressValues.POLICIES, TableNameValues.POLICIES);

            if (!policies.Any())
            {
                result.NotValidResponse(MessageValues.POLICIES_REPOSITORY_EMPTY);

                return result;
            }

            result.IsValid = true;
            result.Models = policies.Select(c => (Policy)new Policy().InjectFrom(c));

            return result;
        }
    }
}