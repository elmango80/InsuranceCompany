
using MNG.Domain.Entities;
using MNG.Domain.Values;

using NUnit.Framework;

namespace MNG.Infrastructure.Test
{
    [TestFixture]
    class ResourcesHandlerShould
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
        }

        [Test]
        public void ArgumentNullException_IfAnyParametersIsNullOrEmpty()
        {
            var result = ResourcesHandler.Load<Client>(string.Empty, string.Empty);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ErrorMessageValues.ARGUMENT_NULL, result.Message);
        }
    }
}
