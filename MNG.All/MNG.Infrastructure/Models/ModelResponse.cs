namespace MNG.Infrastructure.Models
{
    public class ModelResponse<T> : BaseResponse where T : class, new()
    {
        public ModelResponse()
        {
            Model = new T();
        }

        public T Model { get; set; }
    }
}
