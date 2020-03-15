namespace MNG.API.Code.Configuration
{
    public class TokenSetting
    {
        public string JwtKey { get; set; }
        
        public string JwtIssuer { get; set; }
        
        public string JwrAudience { get; set; }
        
        public int JwtExpireHours { get; set; }
    }
}
