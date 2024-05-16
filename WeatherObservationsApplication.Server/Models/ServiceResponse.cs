namespace WeatherObservationsApplication.Server.Models
{
    public class ServiceResponse
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
