using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using MNG.Infrastructure.Models;

namespace MNG.Infrastructure.Extensions
{
    public static class ExceptionExtension
    {
        public static BaseResponse HandlerException(this Exception exception, ILogger logger)
        {
            if (exception == null)
            {
                return new BaseResponse()
                {
                    IsValid = false,
                    Message = $"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] UNKNOWN |" +
                    "An error has occurred, but the cause could not be catch."
                };
            }

            logger.LogError(
                $"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}] {exception.Source} | {exception.Message} " +
                $"| Type: {exception.GetType()}, Trace: {exception.StackTrace}");

            return new BaseResponse()
            {
                IsValid = false,
                Message = exception.Message,
            };
        }
    }
}
