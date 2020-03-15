using System;
using System.Collections.Generic;

using MNG.Application.Contracts;
using MNG.Application.DTOs;
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
    public class ClientServiceShould
    {
        private readonly string idClientNotExists = "e8fd159b-57c4-4d36-9bd7-a59ca13057bb";
        private readonly string idClientExists = "a0ece5db-cd14-4f21-812f-966633e7be86";
        private readonly string idPolicyNotLinked = "a0ece5db-cd14-4f21-812f-966633e7be86";
        private readonly string idPolicyLinked = "7b624ed3-00d5-4c1b-9ab8-c265067ef58b";

        private Mock<IRepository<Client>> _clientsRepositoryMock;
        private Mock<IPolicyService> _policyServiceMock;
        private IClientService _clientService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var data = new ModelsResponse<Client>
            {
                IsValid = true,
                Message = string.Empty,
                Models = new List<Client>
                {
                    new Client
                    {
                        IdClient = "a0ece5db-cd14-4f21-812f-966633e7be86",
                        Name = "Britney",
                        Email = "britneyblankenship@quotezart.com",
                        Role = "admin"
                    }
                }
            };

            var policyLinked = new ModelResponse<PolicyDTO>
            {
                IsValid = true,
                Message = string.Empty,
                Model = new PolicyDTO
                {
                    IdPolicy = "7b624ed3-00d5-4c1b-9ab8-c265067ef58b",
                    AmountInsured = 399.89M,
                    Email = "inesblankenship@quotezart.com",
                    InceptionDate = new DateTime(2015, 7, 6, 6, 55, 49),
                    InstallmentPayment = true,
                    IdClient = "a0ece5db-cd14-4f21-812f-966633e7be86"
                },
            };

            _clientsRepositoryMock = new Mock<IRepository<Client>>();
            _clientsRepositoryMock.Setup(m => m.GetData()).Returns(data);

            _policyServiceMock = new Mock<IPolicyService>();
            _policyServiceMock.Setup(m => m.GetPolicyById(idPolicyLinked)).Returns(policyLinked);
            _policyServiceMock.Setup(m => m.GetPolicyById(idPolicyNotLinked))
                .Returns(new ModelResponse<PolicyDTO> 
                { 
                    IsValid = false, 
                    Message = string.Format(MessageValues.POLICY_NOT_FOUND, "id", idPolicyNotLinked) 
                });

            _clientService = new ClientService(_clientsRepositoryMock.Object, _policyServiceMock.Object);
        }

        [Test]
        [Category("CheckingExceptions")]
        public void GetClientById_IfParameterIsNullOrEmpty_ReturnArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _clientService.GetClientById(It.IsAny<string>()));
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetClientById_IfIdClientNoExist_ReturnMessageClientNotFound()
        {
            string id = idClientNotExists;
            var result = _clientService.GetClientById(id);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(string.Format(MessageValues.CLIENT_NOT_FOUND, nameof(id), id), result.Message);
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetClientById_IfIdClientExist_ReturnClientModel()
        {
            string id = idClientExists;
            var result = _clientService.GetClientById(id);

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(id, result.Model.IdClient);
        }

        [Test]
        [Category("CheckingExceptions")]
        public void GetClientByIdPolicy_IfParameterIsNullOrEmpty_ReturnArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _clientService.GetClientByIdPolicy(It.IsAny<string>()));
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetClientByIdPolicy_IfIdPolicyNotLinked_ReturnMessagePolicyNotFound()
        {
            string id = idPolicyNotLinked;
            var result = _clientService.GetClientByIdPolicy(id);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(string.Format(MessageValues.POLICY_NOT_FOUND, nameof(id), id), result.Message);
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void GetClientByIdPolicy_IfIdPolicyLinked_ReturnMessagePolicyNotFound()
        {
            string id = idPolicyLinked;
            var result = _clientService.GetClientByIdPolicy(id);

            Assert.IsTrue(result.IsValid);
            Assert.IsNotNull(result.Model.IdClient);
        }

    }
}
