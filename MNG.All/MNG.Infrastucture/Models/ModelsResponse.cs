using System;
using System.Collections.Generic;
using System.Linq;

namespace MNG.Infrastructure.Models
{
    public class ModelsResponse<T> : BaseResponse where T : class
    {
        public IEnumerable<T> Models { get; set; }

        public ModelsResponse()
        {
            Models = Enumerable.Empty<T>();
        }
    }
}
