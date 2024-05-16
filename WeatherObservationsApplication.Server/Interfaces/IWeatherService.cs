namespace WeatherObservationsApplication.Server.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string location);
    }
}
