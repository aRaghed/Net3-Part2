using BackgroundWorker.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BackgroundWorker.Services
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddWeatherDb(() => context.Configuration.GetConnectionString("WeatherDb"), ServiceLifetime.Transient);

            //This will generate a registration of interface + implementation and create a HttpClient specific for that registration
            services.AddHttpClient<ITypedHttpClientService, TypedHttpClientService>(ConfigureUnsecureHttpClientOptions);

            //This will generate unbound HttpClients that you can get from the IHttpClientFactory.CreateClient
            services.AddHttpClient("SecureHttpClient", ConfigureSecureHttpClientOptions);
            services.AddHttpClient("UnsecureHttpClient", ConfigureUnsecureHttpClientOptions);
            services.AddTransient<IOrdinaryService, OrdinaryService>();

            return services;
        }

        private static void ConfigureSecureHttpClientOptions(IServiceProvider provider, HttpClient client)
        {
            HttpClientSettings options = provider.GetService<IConfiguration>()
                    .GetSection(HttpClientSettings.Section)
                    .Get<HttpClientSettings>();

            client.BaseAddress = new Uri(options.Url);

            //https://stackoverflow.com/questions/30858890/how-to-use-httpclient-to-post-with-authentication
            var byteArray = new UTF8Encoding().GetBytes($"{options.ClientId}:{options.ClientSecret}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        private static void ConfigureUnsecureHttpClientOptions(IServiceProvider provider, HttpClient client)
        {
            HttpClientSettings options = provider.GetService<IConfiguration>()
                    .GetSection(HttpClientSettings.Section)
                    .Get<HttpClientSettings>();

            client.BaseAddress = new Uri(options.Url);
        }
    }
}