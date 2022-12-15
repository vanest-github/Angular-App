using OddsCalculator.Service;
using OddsCalculator.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace OddsCalculator.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IOddsCalculatorService, OddsCalculatorService>();
        }
    }
}
