using System;
using Microsoft.Extensions.Logging;
using MNG.Infrastructure.Models;

namespace MNG.Infrastructure.Helpers
{
    public static class ResponseHelper
    {
        public static void NotValidResponse(this BaseResponse response, string message)
        {
            response.HandlerResponse(message, false);

        }

        public static void ValidResponse(this BaseResponse response, string message)
        {
            response.HandlerResponse(message, true);
        }

        private static void HandlerResponse(this BaseResponse response, string message, bool isValid)
        {
            response.IsValid = isValid;
            response.Message = message;
        }
    }
}
