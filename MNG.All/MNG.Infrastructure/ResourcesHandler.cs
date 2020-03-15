using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

using MNG.Domain.Values;

using Newtonsoft.Json;
using Omu.ValueInjecter;

namespace MNG.Infrastructure
{
    public static class ResourcesHandler
    {
        public static IEnumerable<T> Load<T>(string address, string tableName) where T : class, new()
        {
            string data = string.Empty;

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException(null, MessageValues.ARGUMENT_NULL);
            }

            try
            {
                using WebClient webClient = new WebClient();

                data = webClient.DownloadString(address);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new JsonReaderException(string.Format(MessageValues.JSON_READER, nameof(T).ToLower()));
            }

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(data);
            DataTable dataTable = dataSet.Tables[tableName];

            if (dataTable == null)
            {
                throw new NullReferenceException();
            }

            var datas = dataTable.AsEnumerable().Select(t => (T)new T().InjectFrom(t));

            data = JsonConvert.SerializeObject(dataTable);

            return JsonConvert.DeserializeObject<IEnumerable<T>>(data);
            //return dataTable.AsEnumerable().Select(t => (T)new T().InjectFrom(t));
        }
    }
}
