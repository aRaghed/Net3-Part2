using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using WeatherDb.Model;

namespace BackgroundWorker.Services
{
    public class OrdinaryService : IOrdinaryService
    {
        private readonly ILogger<OrdinaryService> logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient secureHttpClient;
        private readonly HttpClient unsecureHttpClient;
        private readonly HttpClient plainHttpClient;

        public OrdinaryService(ILogger<OrdinaryService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;

            //Preconfigured according to ApplicationServicesExtension.ConfigureSecureHttpClientOptions
            secureHttpClient = this.httpClientFactory.CreateClient("SecureHttpClient");

            //Preconfigured according to ApplicationServicesExtension.ConfigureUnsecureHttpClientOptions
            unsecureHttpClient = this.httpClientFactory.CreateClient("UnsecureHttpClient");

            //This will not be preconfigured
            plainHttpClient = this.httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            logger.LogInformation("GetWeatherForecasts running at: {time}", DateTimeOffset.Now);

            string json = await secureHttpClient.GetStringAsync("/weatherforecast");
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(json, options);
        }

        public async Task<WeatherForecast> GetWeatherForecast()
        {
            logger.LogInformation("GetWeatherForecast running at: {time}", DateTimeOffset.Now);

            string json = await secureHttpClient.GetStringAsync("/weatherforecast/now");
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<WeatherForecast>(json, options);
        }
    }
}