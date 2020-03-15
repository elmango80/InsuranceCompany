using System;
using Microsoft.Extensions.Logging;
using MNG.Infrastructure.Models;

namespace MNG.Infrastructure.Helpers
{
    public static class ResponseHelper
    {
        public static void NotValidResponse(this ResponseBase response, string message)
        {
            response.HandlerResponse(message, false);
        }

        public static void ValidResponse(this ResponseBase response, string message)
        {
            response.HandlerResponse(message, true);
        }

        private static void HandlerResponse(this ResponseBase response, string message, bool isValid)
        {
            response.IsValid = isValid;
            response.Message = message;
        }
    }
}
