using System.Collections.Generic;
using System.Linq;

using MNG.Domain.Entities;
using MNG.Domain.Values;
using MNG.Infrastructure.Contracts;
using MNG.Infrastructure.Models;
using MNG.Infrastucture.Helpers;
using Omu.ValueInjecter;

namespace MNG.Infrastructure.Repositories
{
    public class ClientsRespository : IRepository<Client>
    {
        public ModelsResponse<Client> GetData()
        {
            var result = new ModelsResponse<Client>();
            var clients = ResourcesHandler.Load<Client>(AddressValues.CLIENTS, TableNameValues.CLIENTS);

            if (!clients.IsValid)
            {
                result.NotValid(clients.Message);

                return result;
            }

            result.IsValid = true;
            result.Models = clients.Models.Select(c => (Client)new Client().InjectFrom(c));

            return result;
        }
    }
}