using System;
using System.Collections.Generic;
using System.Linq;

using MNG.Application.Contracts;
using MNG.Application.Services;
using MNG.Domain.Entities;
using MNG.Domain.Values;
using MNG.Infrastructure.Contracts;
using MNG.Infrastructure.Models;

using Moq;

using NUnit.Framework;

namespace MNG.Application.UnitTest
{
    [TestFixture]
    public class PolicyServiceShould
    {
        private readonly string idPolicyNotExists = "7b624ed3-00d5-4c1b-9ab8-c265067ef58b";
        private readonly string idPolicyExists = "64cceef9-3a01-49ae-a23b-3761b604800b";
        private readonly string idClientNotExists = "a0ece5db-cd14-4f21-812f-966633e7be86";
        private readonly string idClientExists = "e8fd159b-57c4-4d36-9bd7-a59ca13057bb";

        private Mock<IRepository<Policy>> _policiesRepositoryMock;
        private IPolicyService _policyService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var data = new ModelsResponse<Policy>
            {
                IsValid = true,
                Message = string.Empty,
                Models = new List<Policy>
                {
                    new Policy
                    {
                        IdPolicy = "64cceef9-3a01-49ae-a23b-3761b604800b",
                        AmountInsured = 1825.89M,
                        Email = "inesblankenship@quotezart.com",
                        InceptionDate = new DateTime(2016, 6, 1, 3, 33, 32),
                        InstallmentPayment = true,
                        IdClient = "e8fd159b-57c4-4d36-9bd7-a59ca13057bb"
                    }
                }
            };

            _policiesRepositoryMock = new Mock<IRepository<Policy>>();
            _policiesRepositoryMock.Setup(m => m.GetData()).Returns(data);

            _policyService = new PolicyService(_policiesRepositoryMock.Object);
        }

        [Test]
        [Category("CheckingExceptions")]
        public void GetPolicyById_IfParameterIsNullOrEmpty_ReturnArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _policyService.GetPolicyById(It.IsAny<string>()));
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetPolicyByID_IfIdPolicyNotExists_ReturnMessagePolicyNotFound()
        {
            string id = idPolicyNotExists;
            var result = _policyService.GetPolicyById(id);

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(string.Format(MessageValues.POLICY_NOT_FOUND, nameof(id), id), result.Message);
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetPolicyByID_IfIdPolicyExists_ReturnPolicyModel()
        {
            string id = idPolicyExists;
            var result = _policyService.GetPolicyById(id);

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(id, result.Model.IdPolicy);
        }

        [Test]
        [Category("CheckingExceptions")]
        public void GetPoliciesByIdClient_IfParameterIsNullOrEmpty_ReturnArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _policyService.GetPoliciesByIdClient(It.IsAny<string>()));
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetPoliciesByIdClient_IfIdClientNotExist_ReturnMessageNotLinkedPolicies()
        {
            string idClient = idClientNotExists;
            var result = _policyService.GetPoliciesByIdClient(idClient);

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(MessageValues.CLIENT_NOT_LINKED_POLICIES, result.Message);
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetPoliciesByIdClient_IfIdClientExist_ReturnPoliciesLinkedModel()
        {
            string idClient = idClientExists;
            var result = _policyService.GetPoliciesByIdClient(idClient);

            Assert.IsTrue(result.IsValid);
            Assert.IsNotEmpty(result.Models);
        }
    }
}