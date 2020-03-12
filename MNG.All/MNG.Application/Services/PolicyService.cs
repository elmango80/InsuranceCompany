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

        public ModelResponse<PolicyDTO> GetPolicyById(string id)
        {
            var result = new ModelResponse<PolicyDTO>();

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var policies = _policiesRepository.GetData();
            var policy = policies.Models.SingleOrDefault(p => p.IdPolicy == id);

            if (policy == null)
            {
                result.ValidResponse(string.Format(MessageValues.POLICY_NOT_FOUND, nameof(id), id));

                return result;
            }

            result.IsValid = true;
            result.Model = (PolicyDTO)new PolicyDTO().InjectFrom(policy);

            return result;
        }

        public ModelsResponse<PolicyDTO> GetPoliciesByIdClient(string idClient)
        {
            var result = new ModelsResponse<PolicyDTO>();

            if (string.IsNullOrEmpty(idClient))
            {
                throw new ArgumentNullException(string.Empty, MessageValues.ARGUMENT_NULL);
            }

            var policies = _policiesRepository.GetData();
            var policiesLinked = policies.Models.Where(p => p.IdClient == idClient);

            if (!policiesLinked.Any())
            {
                result.ValidResponse(MessageValues.CLIENT_NOT_LINKED_POLICIES);

                return result;
            }

            result.IsValid = true;
            result.Models = policiesLinked.Select(pl => (PolicyDTO)new PolicyDTO().InjectFrom(pl));

            return result;
        }
    }
}
