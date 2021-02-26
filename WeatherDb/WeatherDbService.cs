using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using WeatherDb.Abstract;
using WeatherDb.Model;

namespace WeatherDb
{
    public class WeatherDbService : IWeatherDbService
    {
        private readonly IWeatherDbContext unitOfWork;

        public WeatherDbService(IWeatherDbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddWeatherForecastAsync(WeatherForecast forecast)
        {
            await unitOfWork.Repository<WeatherForecast>().InsertAsync(forecast);
            await unitOfWork.SaveChangesAsync();
        }

        public Task<IEnumerable<WeatherForecast>> GetLatestHourAsync()
        {
            Expression<Func<WeatherForecast, bool>> func = a => a.Date >= DateTime.Now.AddHours(-1);

            var result = unitOfWork.Repository<WeatherForecast>().Get(func);

            return Task.FromResult(result);
        }

        public Task<IEnumerable<WeatherForecast>> GetPeriodAsync(DateTime start, DateTime stop)
        {
            Expression<Func<WeatherForecast, bool>> func = a => a.Date >= start && a.Date <= stop;

            var result = unitOfWork.Repository<WeatherForecast>().Get(func);

            return Task.FromResult(result);
        }
    }
}