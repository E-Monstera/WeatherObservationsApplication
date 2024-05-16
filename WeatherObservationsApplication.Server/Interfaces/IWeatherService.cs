using WeatherObservationsApplication.Server.Models;

namespace WeatherObservationsApplication.Server.Interfaces
{
    public interface IWeatherService
    {
        Task<ServiceResponse> GetWeatherAsync(string location);
    }
}
