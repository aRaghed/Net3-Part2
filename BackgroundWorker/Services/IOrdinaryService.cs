using System.Collections.Generic;
using System.Threading.Tasks;

using WeatherDb.Model;

namespace BackgroundWorker.Services
{
    public interface IOrdinaryService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
    }
}