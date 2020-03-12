using Newtonsoft.Json;

namespace MNG.API.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public string IdClient { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
