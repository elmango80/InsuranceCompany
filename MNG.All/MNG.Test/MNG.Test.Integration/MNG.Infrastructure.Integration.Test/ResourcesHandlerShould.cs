using System;
using System.Collections;
using System.Net;

using MNG.Domain.Entities;
using MNG.Domain.Values;

using Moq;

using NUnit.Framework;

namespace MNG.Infrastructure.Integration.Test
{
    [TestFixture]
    class ResourcesHandlerShould
    {
        public readonly string ADDRESS_NOT_EXIST = "http://www.mocky.io/v2/0000000000000000000a00aa";
        public readonly string TABLE_NAME_NOT_EXIST = "NotExists";

        private static object[] ArgumentsNullCases =
        {
            new string[] { It.IsAny<string>(), It.IsAny<string>() },
            new string[] { string.Empty, string.Empty },
            new string[] { It.IsAny<string>(), TableNameValues.CLIENTS },
            new string[] { AddressValues.CLIENTS, It.IsAny<string>() }
        };

        [Test, TestCaseSource("ArgumentsNullCases")]
        [Category("CheckingExceptions")]
        public void Load_ArgumentNullException_IfAnyParametersIsNullOrEmpty(string address, string tableName)
        {
            Assert.Throws<ArgumentNullException>(() => ResourcesHandler.Load<Client>(address, tableName));
            Assert.Throws<ArgumentNullException>(() => ResourcesHandler.Load<Policy>(address, tableName));
        }

        [Test]
        [Category("CheckingExceptions")]
        public void Load_WebException_IfAddressNotExists()
        {
            Assert.Throws<WebException>(() => ResourcesHandler.Load<Client>(ADDRESS_NOT_EXIST, TABLE_NAME_NOT_EXIST));
            Assert.Throws<WebException>(() => ResourcesHandler.Load<Policy>(ADDRESS_NOT_EXIST, TABLE_NAME_NOT_EXIST));
        }

        [Test]
        [Category("CheckingExceptions")]
        public void Load_NullReferenceException_IfTableNameNotExists()
        {
            Assert.Throws<NullReferenceException>(() => ResourcesHandler.Load<Client>(AddressValues.CLIENTS, TABLE_NAME_NOT_EXIST));
            Assert.Throws<NullReferenceException>(() => ResourcesHandler.Load<Policy>(AddressValues.POLICIES, TABLE_NAME_NOT_EXIST));
        }

        [Test]
        [Category("CheckingFunctionality")]
        public void LoadClients_ResponseClientsData_IfAddressAndTableNameExists()
        {
            var result = ResourcesHandler.Load<Client>(AddressValues.CLIENTS, TableNameValues.CLIENTS);

            Assert.IsNotEmpty(result);
        }

        [TestCase]
        [Category("CheckingFunctionality")]
        public void LoadPolicies_ResponsePoliciesData_IfAddressAndTableNameExists()
        {
            var result = ResourcesHandler.Load<Policy>(AddressValues.POLICIES, TableNameValues.POLICIES);

            Assert.IsNotEmpty(result);
        }
    }
}
