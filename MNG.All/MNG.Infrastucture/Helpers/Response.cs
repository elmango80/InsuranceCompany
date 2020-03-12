using System;

using MNG.Infrastructure.Models;

namespace MNG.Infrastucture.Helpers
{
    public static class Response
    {
        public static object TimeDate { get; private set; }

        public static void HandlerException(
            this BaseResponse response,
            Exception exception,
            string message,
            string source,
            string method)
        {
            if (exception == null)
            {
                return;
            }

            //TODO: Log error when occurred exception
            //Logger.GetLogger().WriteError($"{source} - {method}", exception.Message, exception);
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] {source} - {method} | Exception type: {exception.GetType()}, Message: {exception.Message}");

            response.NotValid(message);
        }

        public static void NotValid(this BaseResponse response, string message)
        {
            response.IsValid = false;
            response.Message = message;
        }
    }
}
