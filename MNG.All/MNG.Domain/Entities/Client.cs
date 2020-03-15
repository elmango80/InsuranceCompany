using Newtonsoft.Json;

namespace MNG.Domain.Entities
{
    public class Client
    {
        [JsonProperty("id")]
        public string IdClient { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
