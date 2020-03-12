namespace MNG.Infrastructure.Models
{
    public class ModelResponse<T> : BaseResponse where T : class, new()
    {
        public T Model { get; set; }

        public ModelResponse()
        {
            Model = default;
        }
    }
}
