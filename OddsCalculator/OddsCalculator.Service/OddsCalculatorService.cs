using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OddsCalculator.Entity;
using OddsCalculator.Service.Helper;
using OddsCalculator.Service.Interfaces;
using OddsCalculator.Service.ServiceResponse;

namespace OddsCalculator.Service
{
    public class OddsCalculatorService : IOddsCalculatorService
    {
        private readonly IOptions<InputCount> _maxCountConfig;

        public OddsCalculatorService(IOptions<InputCount> maxCountConfig)
        {
            _maxCountConfig = maxCountConfig;
        }

        /// <summary>
        /// Gets the possible odds for the given count combination.
        /// </summary>
        /// <param name="totalCount">Total number of balls.</param>
        /// <param name="drawnCount">Number of balls drawn.</param>
        /// <returns>Array of values indicating combination of odds.</returns>
        public async Task<ServiceResult<IEnumerable<MatchedOdds>>> GetOddsAsync(int totalCount, int drawnCount)
        {
            try
            {
                var inputCount = new InputCount { TotalCount = totalCount, DrawnCount = drawnCount };
                if (!this.IsValidInputCount(inputCount))
                {
                    return ServiceResult<IEnumerable<MatchedOdds>>.CreateError(400);
                }

                var odds = await OddsHelper.CalculateOddsAsync(inputCount).ConfigureAwait(false);
                var matchedOdds = odds.Select((x, i) => new MatchedOdds { MainMatch = i + 1, PerOddsCount = x })
                                      .OrderByDescending(x => x.MainMatch);
                return ServiceResult<IEnumerable<MatchedOdds>>.CreateSuccess(matchedOdds);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<MatchedOdds>>.CreateError(500, ex);
            }
        }

        private bool IsValidInputCount(InputCount inputCount)
        {
            if (inputCount.TotalCount < 1 || inputCount.DrawnCount < 1 || inputCount.TotalCount < inputCount.DrawnCount)
            {
                return false;
            }

            if (inputCount.TotalCount > _maxCountConfig.Value.TotalCount || inputCount.DrawnCount > _maxCountConfig.Value.DrawnCount)
            {
                return false;
            }

            return true;
        }
    }
}
