using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OddsCalculator.Entity;

namespace OddsCalculator.Api.Extensions
{
    public static class ConfigExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InputCount>(configuration.GetSection("MaxCount"));
        }
    }
}
