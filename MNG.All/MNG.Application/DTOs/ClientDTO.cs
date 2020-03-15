using Newtonsoft.Json;

namespace MNG.Application.DTOs
{
    public class ClientDTO
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
