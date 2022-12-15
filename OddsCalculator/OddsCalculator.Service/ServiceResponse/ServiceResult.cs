using System;
using System.Net;

namespace OddsCalculator.Service.ServiceResponse
{
    public class ServiceResult<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the service call is success.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or Sets the response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets the exceptions.
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Creates the success response.
        /// </summary>
        /// <returns>Service result object with data.</returns>
        public static ServiceResult<T> CreateSuccess(T data, bool isSuccess = true)
        {
            return new ServiceResult<T>()
            {
                IsSuccess = isSuccess,
                Data = data,
                StatusCode = HttpStatusCode.OK,
            };
        }

        /// <summary>
        /// Creates the error response.
        /// </summary>
        /// <returns>Service result object with exception code.</returns>
        public static ServiceResult<T> CreateError(int statusCode, Exception exception = null)
        {
            return new ServiceResult<T>()
            {
                IsSuccess = false,
                Data = default,
                StatusCode = (HttpStatusCode)statusCode,
                Error = exception,
            };
        }
    }
}
