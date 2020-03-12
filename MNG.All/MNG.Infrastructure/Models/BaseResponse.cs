namespace MNG.Infrastructure.Models
{
    public class BaseResponse
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public BaseResponse()
        {
            IsValid = false;
            Message = string.Empty;
        }
    }
}
