using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MNG.Domain.Entities
{
    public class Policy
    {
        [JsonProperty("id")]
        public string IdPolicy { get; set; }

        [JsonProperty("clientId")]
        public string IdClient { get; set; }

        [JsonProperty("amountInsured")]
        public decimal AmountInsured { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("inceptionDate")]
        public DateTime InceptionDate { get; set; }

        [JsonProperty("installmentPayment")]
        public bool InstallmentPayment { get; set; }
    }
}
