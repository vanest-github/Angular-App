using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OddsCalculator.Entity;

namespace OddsCalculator.Service.Helper
{
    public static class OddsHelper
    {
        /// <summary>
        /// Calculates the possible odds for the given count combination.
        /// </summary>
        /// <param name="inputCount">InputCount object containing counts of balls.</param>
        /// <returns>Array of values indicating combination of odds.</returns>
        public static async Task<IEnumerable<double>> CalculateOddsAsync(InputCount inputCount)
        {
            var n_r = inputCount.TotalCount - inputCount.DrawnCount;
            var nCr = await Calculate_nCr_Async(inputCount.TotalCount, inputCount.DrawnCount).ConfigureAwait(false);
            var odds = new double[inputCount.DrawnCount];
            for (int m = 1; m <= inputCount.DrawnCount; m++)
            {
                var rCm = await Calculate_nCr_Async(inputCount.DrawnCount, m).ConfigureAwait(false);
                var n_rCr_m = await Calculate_nCr_Async(n_r, inputCount.DrawnCount - m).ConfigureAwait(false);
                odds[m - 1] = nCr / (rCm * n_rCr_m);
            }

            return odds.Select(x => Math.Round(x, 0));
        }

        private static async Task<double> Calculate_nCr_Async(int n, int r)
        {
            if (n == r || r == 0)
                return 1;
            if (r == 1)
                return n;

            var dividend = await Task.Run(() => GetFactorial(n, Math.Max(r, n - r) + 1)).ConfigureAwait(false);
            var divisor = await Task.Run(() => GetFactorial(Math.Min(r, n - r))).ConfigureAwait(false);

            return dividend / divisor;
        }

        private static double GetFactorial(int inputNumber, int terminator = 1)
        {
            if (inputNumber <= terminator)
                return terminator;
            return inputNumber * GetFactorial(inputNumber - 1, terminator);
        }
    }
}
