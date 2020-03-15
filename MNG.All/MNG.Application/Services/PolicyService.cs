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
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy> _policiesRepository;

        public PolicyService(IRepository<Policy> policiesRepository)
        {
            _policiesRepository = policiesRepository;
        }

        public ResponseModel<PolicyDTO> GetPolicyById(string id)
        {
            var result = new ResponseModel<PolicyDTO>();

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var policies = _policiesRepository.GetData();
            var policy = policies.Models.SingleOrDefault(p =>
                string.Equals(p.IdPolicy, id, StringComparison.InvariantCultureIgnoreCase));

            if (policy == null)
            {
                result.NotValidResponse(string.Format(MessageValues.POLICY_NOT_FOUND, nameof(id), id));

                return result;
            }

            result.IsValid = true;
            result.Model = (PolicyDTO)new PolicyDTO().InjectFrom(policy);

            return result;
        }

        public ResponseModels<PolicyDTO> GetPoliciesLinkedByIdClient(string idClient)
        {
            var result = new ResponseModels<PolicyDTO>();

            if (string.IsNullOrEmpty(idClient))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var policies = _policiesRepository.GetData();
            var policiesLinked = policies.Models.Where(p => p.IdClient == idClient);

            if (!policiesLinked.Any())
            {
                result.NotValidResponse(MessageValues.CLIENT_NOT_LINKED_POLICIES);

                return result;
            }

            result.IsValid = true;
            result.Models = policiesLinked.Select(pl => (PolicyDTO)new PolicyDTO().InjectFrom(pl));

            return result;
        }
    }
}