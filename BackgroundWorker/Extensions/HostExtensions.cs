using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;

using WeatherDb.Abstract;

namespace BackgroundWorker.Extensions
{
    public static class HostExtensions
    {
        public static IHost EnsureDbExist(this IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            IHostEnvironment env = serviceProvider.GetService<IHostEnvironment>();
            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            IWeatherDbContext dbContext = serviceProvider.GetRequiredService<IWeatherDbContext>();

            logger.LogInformation("Migration start");

            dbContext.EnsureDbExists(env.IsDevelopment());

            logger.LogInformation("Migrations complete");

            return host;
        }
    }
}