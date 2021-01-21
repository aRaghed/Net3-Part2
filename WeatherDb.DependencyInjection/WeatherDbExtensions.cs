using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using System;

using WeatherDb;
using WeatherDb.Abstract;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WeatherDbExtensions
    {
        private const string defaultWeatherDb = "Server=(localdb)\\mssqllocaldb;Database=WeatherDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static IHostBuilder AddWeatherDb(this IHostBuilder hostBuilder,
            Func<string> GetConnectionString = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            return hostBuilder.ConfigureServices((context, services) =>
                services.AddWeatherDb(GetConnectionString, serviceLifetime));
        }

        public static IServiceCollection AddWeatherDb(this IServiceCollection services,
            Func<string> GetConnectionString = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            string connectionString = GetConnectionString != null ?
                GetConnectionString() :
                defaultWeatherDb;

            services.AddDbContext<IUnitOfWork, WeatherDbContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(connectionString), serviceLifetime, serviceLifetime);

            services.AddTransient<IWeatherDbService, WeatherDbService>();

            return services;
        }
    }
}