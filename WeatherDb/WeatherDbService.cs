using System.Threading.Tasks;

using WeatherDb.Abstract;
using WeatherDb.Model;

namespace WeatherDb
{
    public class WeatherDbService : IWeatherDbService
    {
        private readonly IUnitOfWork unitOfWork;

        public WeatherDbService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddWeatherForecastAsync(WeatherForecast forecast)
        {
            await unitOfWork.Repository<WeatherForecast>().InsertAsync(forecast);
            await unitOfWork.SaveChangesAsync();
        }
    }
}