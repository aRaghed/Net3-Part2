using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Linq;

using WeatherDb.Abstract;

namespace BackgroundWorker.Extensions
{
    public static class HostExtensions
    {
        public static IHost EnsureDbExist(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("Migration start");

            var dbContext = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var migrations = dbContext.DatabaseAccessor.GetPendingMigrations();

            logger.LogInformation("Applying {migrations} migrations to database", migrations.Count());
            if (migrations.Any())
            {
                dbContext.DatabaseAccessor.Migrate();
            }

            logger.LogInformation("Migrations complete"); return host;
        }
    }
}
