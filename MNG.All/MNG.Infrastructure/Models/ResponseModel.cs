namespace MNG.Infrastructure.Models
{
    public class ResponseModel<T> : ResponseBase where T : class, new()
    {
        public T Model { get; set; }

        public ResponseModel()
        {
            Model = default;
        }
    }
}
