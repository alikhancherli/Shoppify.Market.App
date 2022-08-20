using Microsoft.AspNetCore.Http;

namespace Shoppify.Market.App.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }

        public CustomException(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
