using System.Net;

namespace OddsCalculator.Api.ApiResponses
{
    /// <summary>
    /// The api response.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or Sets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets the data.
        /// </summary>
        public object Data { get; set; }
    }
}