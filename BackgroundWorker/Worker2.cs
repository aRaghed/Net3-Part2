using BackgroundWorker.Configuration;
using BackgroundWorker.Services;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

using WeatherDb.Abstract;

namespace BackgroundWorker
{
    public class Worker2 : BackgroundService
    {
        private readonly ILogger<Worker2> logger;
        private readonly ITypedHttpClientService service;
        private readonly IWeatherDbService weatherDbService;
        private readonly TimerSettings timerSettings;
        private readonly TimeSpan timeSpan;

        public Worker2(ILogger<Worker2> logger, IOptions<TimerSettings> options, ITypedHttpClientService service, IWeatherDbService weatherDbService)
        {
            this.logger = logger;
            this.service = service;
            this.weatherDbService = weatherDbService;
            timerSettings = options.Value;
            timeSpan = TimeSpan.Parse(timerSettings.TimeSpan);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var weatherForecasts = await service.GetWeatherForecasts();
                foreach (var forecast in weatherForecasts)
                {
                    //await weatherDbService.AddWeatherForecastAsync(forecast);
                    logger.LogInformation("{date} - {summary}, {celcius}C, {farenheit}F", forecast.Date, forecast.Summary, forecast.TemperatureC, forecast.TemperatureF);
                }
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(timeSpan, cancellationToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}