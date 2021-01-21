using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace MultipleRegistrationsSample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddSingleton<IConfiguration>(configuration)
                .AddScoped<IService, Service1>()
                .AddScoped<IService, Service2>()
                .AddScoped<StartUp>()
                .BuildServiceProvider();

            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

            using IServiceScope scope = serviceProvider.CreateScope();
            await scope.ServiceProvider
                .GetRequiredService<StartUp>()
                .RunAsync();
        }
    }
}