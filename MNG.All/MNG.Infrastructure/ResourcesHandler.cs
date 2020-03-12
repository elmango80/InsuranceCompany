using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

using MNG.Domain.Values;
using MNG.Infrastructure.Models;
using MNG.Infrastructure.Helpers;

using Newtonsoft.Json;

namespace MNG.Infrastructure
{
    public static class ResourcesHandler
    {
        public static ModelsResponse<T> Load<T>(string address, string tableName) where T : class, new()
        {
            var result = new ModelsResponse<T>();
            string data = string.Empty;

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException(string.Empty, ErrorMessageValues.ARGUMENT_NULL);
            }

            using var webClient = new WebClient();
            {
                data = webClient.DownloadString(address);

                if (string.IsNullOrEmpty(data))
                {
                    throw new JsonReaderException(string.Format(ErrorMessageValues.JSON_READER, nameof(T).ToLower()));
                }

                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(data);
                DataTable dataTable = dataSet.Tables[tableName];

                if (dataTable == null)
                {
                    throw new NullReferenceException();
                }

                data = JsonConvert.SerializeObject(dataTable);

                result.IsValid = true;
                result.Models = JsonConvert.DeserializeObject<IEnumerable<T>>(data);
            }

            return result;
        }
    }
}
