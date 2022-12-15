using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OddsCalculator.Api.ApiResponses;
using OddsCalculator.Service.ServiceResponse;

namespace OddsCalculator.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// CreateResponse for controller.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="task">task to exexute and return result.</param>
        /// <returns>A task representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> CreateResponse<T>(Task<ServiceResult<T>> task)
        {
            var result = await task.ConfigureAwait(false);
            var apiResponse = CreateApiResponse(result);
            return this.StatusCode((int)apiResponse.StatusCode, apiResponse.Data);
        }

        private static ApiResponse CreateApiResponse<T>(ServiceResult<T> result)
        {
            if (!result.IsSuccess)
            {
                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = Value("Invalid input.", result.Data),
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Data = result?.Error,
                    };
                }
            }
            else
            {
                return new ApiResponse()
                {
                    StatusCode = result.StatusCode,
                    Data = Value("Odds calculation successful.", result.Data),
                };
            }
        }

        private static ApiMessage<T> Value<T>(string message, T data)
        {
            var apiMessage = new ApiMessage<T>()
            {
                Message = message,
                Data = data,
            };

            return apiMessage;
        }
    }
}
