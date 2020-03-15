using System;

using Microsoft.Extensions.Logging;

using MNG.Infrastructure.Models;

namespace MNG.Infrastructure.Extensions
{
    public static class ExceptionExtension
    {
        public static ResponseBase HandlerException(this Exception exception, ILogger logger)
        {
            if (exception == null)
            {
                return new ResponseBase()
                {
                    IsValid = false,
                    Message = $"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] UNKNOWN |" +
                    "An error has occurred, but the cause could not be catch."
                };
            }

            logger.LogError(
                $"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] {exception.Source} | {exception.Message} " +
                $"| Type: {exception.GetType()}, Trace: {exception.StackTrace}");

            return new ResponseBase()
            {
                IsValid = false,
                Message = exception.Message,
            };
        }
    }
}
