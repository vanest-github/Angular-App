using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OddsCalculator.Service.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OddsCalculator.Api.Controllers
{
    [Route("api/v{v:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OddsCalculatorController : BaseController
    {
        private readonly IOddsCalculatorService _oddsCalculatorService;

        public OddsCalculatorController(IOddsCalculatorService oddsCalculatorService)
        {
            _oddsCalculatorService = oddsCalculatorService;
        }

        /// <summary>
        /// Calculates the possible odds for the given count combination.
        /// </summary>
        /// <param name="totalCount">Total number of balls.</param>
        /// <param name="drawnCount">Number of balls drawn.</param>
        /// <returns>Array of values indicating combination of odds.</returns>
        [HttpGet]
        [Route("odds/{totalCount}/{drawnCount}")]
        [SwaggerOperation("GetOdds")]
        [SwaggerResponse(statusCode: 200, description: "Success")]
        [SwaggerResponse(statusCode: 400, description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, description: "Server Error")]
        public async Task<IActionResult> GetOdds([FromRoute][Required] int totalCount, [FromRoute][Required] int drawnCount)
        {
            return await this.CreateResponse(_oddsCalculatorService.GetOddsAsync(totalCount, drawnCount));
        }
    }
}
