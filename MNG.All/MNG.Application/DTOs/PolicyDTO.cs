using System;

namespace MNG.Application.DTOs
{
    public class PolicyDTO
    {
        public string IdPolicy { get; set; }

        public string IdClient { get; set; }

        public decimal AmountInsured { get; set; }

        public string Email { get; set; }

        public DateTime InceptionDate { get; set; }

        public bool InstallmentPayment { get; set; }
    }
}
