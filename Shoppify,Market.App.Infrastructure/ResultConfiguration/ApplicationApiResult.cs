using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoppify.Market.App.Infrastructure.ResultConfiguration
{
    public class ApplicationApiResult
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }

        public ApplicationApiResult(int statusCode, bool isSuccess, string? message = null)
        {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = isSuccess;
        }

        public static implicit operator ApplicationApiResult(OkResult result)
        {
            return new ApplicationApiResult(StatusCodes.Status200OK, true);
        }

        public static implicit operator ApplicationApiResult(NotFoundResult result)
        {
            return new ApplicationApiResult(StatusCodes.Status404NotFound, false, "There is nothing to show!");
        }

        public static implicit operator ApplicationApiResult(BadRequestResult result)
        {
            return new ApplicationApiResult(StatusCodes.Status400BadRequest, false);
        }

        public static implicit operator ApplicationApiResult(ContentResult result)
        {
            return new ApplicationApiResult(StatusCodes.Status200OK, false, result.Content ?? "Operation Succeded!");
        }
    }

    public class ApplicationApiResult<TData> : ApplicationApiResult where TData : class
    {
        public TData? Data { get; set; }

        public ApplicationApiResult(TData? data, int statusCode, bool isSuccess, string? message = null) : base(statusCode, isSuccess, message)
        {
            Data = data;
        }

        public static implicit operator ApplicationApiResult<TData>(TData data)
        {
            return new ApplicationApiResult<TData>(data, StatusCodes.Status200OK, true);
        }

        public static implicit operator ApplicationApiResult<TData>(OkResult result)
        {
            return new ApplicationApiResult<TData>(null, StatusCodes.Status200OK, true);
        }

        public static implicit operator ApplicationApiResult<TData>(OkObjectResult result)
        {
            return new ApplicationApiResult<TData>((TData)result.Value, StatusCodes.Status200OK, true);
        }

        public static implicit operator ApplicationApiResult<TData>(BadRequestResult result)
        {
            return new ApplicationApiResult<TData>(null, StatusCodes.Status400BadRequest, false);
        }

        public static implicit operator ApplicationApiResult<TData>(BadRequestObjectResult result)
        {
            return new ApplicationApiResult<TData>((TData)result.Value, StatusCodes.Status400BadRequest, false);
        }

        public static implicit operator ApplicationApiResult<TData>(NotFoundResult result)
        {
            return new ApplicationApiResult<TData>(null, StatusCodes.Status404NotFound, false);
        }

        public static implicit operator ApplicationApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApplicationApiResult<TData>((TData)result.Value, StatusCodes.Status404NotFound, false);
        }

        public static implicit operator ApplicationApiResult<TData>(ContentResult result)
        {
            return new ApplicationApiResult<TData>(null, StatusCodes.Status200OK, true, result.Content);
        }

        public static implicit operator ApplicationApiResult<TData>(ObjectResult result)
        {
            bool isSuccess = result.StatusCode == StatusCodes.Status200OK || result.StatusCode == StatusCodes.Status201Created;
            return new ApplicationApiResult<TData>((TData)result.Value, result.StatusCode!.Value, isSuccess);
        }
    }
}
