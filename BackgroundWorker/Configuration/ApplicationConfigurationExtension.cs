using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundWorker.Configuration
{
    public static class ApplicationConfigurationExtension
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, HostBuilderContext context = default)
        {
            services.Configure<TimerSettings>(settings =>
                context.Configuration.GetSection(TimerSettings.Section).Bind(settings));
            services.Configure<HttpClientSettings>(settings =>
                context.Configuration.GetSection(HttpClientSettings.Section).Bind(settings));

            return services;
        }
    }
}