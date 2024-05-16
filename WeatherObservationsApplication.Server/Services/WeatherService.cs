using WeatherObservationsApplication.Server.Interfaces;
using WeatherObservationsApplication.Server.Models;

namespace WeatherObservationsApplication.Server.Services
{
    public class WeatherService : IWeatherService
    {
        /// <summary>
        /// Get the weather for a specific location
        /// </summary>
        /// <param name="location">The location</param>
        /// <returns>The Serviceresponse</returns>
        public async Task<ServiceResponse> GetWeatherAsync(string location)
        {
            ServiceResponse serviceResponse = new();

            try
            {
                string stationId = GetStationId(location);



                var client = new HttpClient();
                var path = "https://api.weather.gov/stations/" + stationId + "/observations";
                var response = await client.GetAsync(path);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();

                //Map the data and update the status
                serviceResponse.Success = true;
                serviceResponse.Data = MapResponseToDailyTemperature(data);
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.ErrorMessage = ex.Message;
            }


            return serviceResponse;
        }


        //Method to get the station based on the location
        //I could not find the API on Weather.Gov for finding the coordinates, which are then used to grab the station.
        //With more time, a good interim approach would be to use another API to grab the coordinates of the provided location
        //then use a mathematical approach to narrow down the specific station that is closest to the station.
        //I will start working on this approach if I have time
        private string GetStationId(string location)
        {
            return "0007W";
        }

        private List<DailyTemperature> MapResponseToDailyTemperature(object data)
        {
            //Map the data to a list of Daily Temperatures for viewing
            return new List<DailyTemperature>();
        }

    }
}
