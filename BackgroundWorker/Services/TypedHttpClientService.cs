using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using WeatherDb.Model;

namespace BackgroundWorker.Services
{
    public class TypedHttpClientService : ITypedHttpClientService
    {
        private readonly ILogger<TypedHttpClientService> logger;
        private readonly HttpClient httpClient;

        public TypedHttpClientService(ILogger<TypedHttpClientService> logger, HttpClient httpClient)
        {
            this.logger = logger;

            //This will be preconfigured according to ApplicationServicesExtension.ConfigureTypedHttpClientOptions
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            logger.LogInformation("GetWeatherForecasts running at: {time}", DateTimeOffset.Now);

            string json = await httpClient.GetStringAsync("/weatherforecast");
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(json, options);
        }
    }
}