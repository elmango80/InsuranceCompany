using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MNG.API.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string IdClient { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
