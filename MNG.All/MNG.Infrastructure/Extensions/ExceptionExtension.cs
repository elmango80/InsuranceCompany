using System;

using Microsoft.Extensions.Logging;

namespace MNG.Infrastructure.Extensions
{
    public static class ExceptionExtension
    {
        public static string HandlerException(this Exception exception, ILogger logger)
        {
            if (exception == null)
            {
                string message = $"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] UNKNOWN | " +
                    "An error has occurred, but the cause could not be catch.";

                logger.LogError(message);

                return message;
            }

            logger.LogError(
                $"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] {exception.Source} | {exception.Message} " +
                $"| Type: {exception.GetType()}, Trace: {exception.StackTrace}");

            return exception.Message;
        }
    }
}
