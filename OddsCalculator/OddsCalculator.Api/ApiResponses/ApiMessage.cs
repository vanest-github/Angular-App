namespace OddsCalculator.Api.ApiResponses
{
    /// <summary>
    /// Data to return in API Response.
    /// </summary>
    /// <typeparam name="T">Type of data.</typeparam>
    public class ApiMessage<T>
    {
        /// <summary>
        /// Gets or sets the message to return in the API reponse.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data to be returned in the API response.
        /// </summary>
        public T Data { get; set; }
    }
}