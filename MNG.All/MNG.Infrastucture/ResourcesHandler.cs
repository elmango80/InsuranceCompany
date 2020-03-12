using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

using MNG.Infrastructure.Models;
using MNG.Infrastucture.Helpers;

using Newtonsoft.Json;

namespace MNG.Infrastructure
{
    public static class ResourcesHandler
    {
        public static ModelsResponse<T> Load<T>(string address, string tableName) where T : class, new()
        {
            var result = new ModelsResponse<T>();
            string data = string.Empty;
            
            using WebClient webClient = new WebClient();
            try
            {
                if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(tableName))
                {
                    throw new ArgumentNullException();
                }

                data = webClient.DownloadString(address);

                if (string.IsNullOrEmpty(data))
                {
                    throw new JsonReaderException($"No data was obtained regarding {nameof(T).ToLower()}");
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

                return result;
            }
            catch (Exception ex)
            {
                //TODO: Log error when not returning API data
                result.HandlerException(ex, "", nameof(ResourcesHandler), nameof(Load));

                result.IsValid = false;
                result.Message = ex.Message;

                return result;
            }
        }
    }
}
