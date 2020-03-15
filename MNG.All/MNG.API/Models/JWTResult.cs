using System;
using Newtonsoft.Json;

namespace MNG.API.Models
{
    public class JWTResult
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }
    }
}
