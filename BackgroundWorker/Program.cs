using BackgroundWorker.Configuration;
using BackgroundWorker.Extensions;
using BackgroundWorker.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundWorker
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().EnsureDbExist().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                    services.AddApplicationConfiguration(hostContext)
                        .AddApplicationServices(hostContext)
                        .AddHostedService<Worker1>()
                        .AddHostedService<Worker2>());
    }
}