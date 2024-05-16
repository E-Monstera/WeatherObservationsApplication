namespace WeatherObservationsApplication.Server.Models
{
    public class DailyTemperature
    {
        public DateTime Date { get; set; }
        public decimal HighTemperature { get; set; }
        public decimal LowTemperature { get; set; }
    }
}
