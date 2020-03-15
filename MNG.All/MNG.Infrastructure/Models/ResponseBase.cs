namespace MNG.Infrastructure.Models
{
    public class ResponseBase
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public ResponseBase()
        {
            IsValid = false;
            Message = string.Empty;
        }
    }
}
