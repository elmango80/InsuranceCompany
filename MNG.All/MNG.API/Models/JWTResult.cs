using System;

namespace MNG.API.Models
{
    public class JWTResult
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
