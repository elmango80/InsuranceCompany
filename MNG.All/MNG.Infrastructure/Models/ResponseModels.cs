using System.Collections.Generic;
using System.Linq;

namespace MNG.Infrastructure.Models
{
    public class ResponseModels<T> : ResponseBase where T : class
    {
        public IEnumerable<T> Models { get; set; }

        public ResponseModels()
        {
            Models = Enumerable.Empty<T>();
        }
    }
}
