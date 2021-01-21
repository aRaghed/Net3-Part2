using System.Collections.Generic;
using System.Threading.Tasks;

using WeatherDb.Model;

namespace BackgroundWorker.Services
{
    public interface ITypedHttpClientService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
    }
}