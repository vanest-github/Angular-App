using System.Collections.Generic;
using System.Threading.Tasks;
using OddsCalculator.Entity;
using OddsCalculator.Service.ServiceResponse;

namespace OddsCalculator.Service.Interfaces
{
    public interface IOddsCalculatorService
    {
        /// <summary>
        /// Gets the possible odds for the given count combination.
        /// </summary>
        /// <param name="totalCount">Total number of balls.</param>
        /// <param name="drawnCount">Number of balls drawn.</param>
        /// <returns>Array of values indicating combination of odds.</returns>
        Task<ServiceResult<IEnumerable<MatchedOdds>>> GetOddsAsync(int totalCount, int drawnCount);
    }
}
