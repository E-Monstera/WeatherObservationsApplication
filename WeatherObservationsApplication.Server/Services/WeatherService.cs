using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
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
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                var agent = client.DefaultRequestHeaders.UserAgent;

                var path = "https://api.weather.gov/stations/" + stationId + "/observations";
                var response = await client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead);

                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    serviceResponse.ErrorMessage = GetResponseMessage(response);
                    serviceResponse.Success = false;
                } else
                {
                    //Map the data and update the status
                    var data = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(data);
                    serviceResponse.Success = true;
                    serviceResponse.Data = MapResponseToDailyTemperature(obj);

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.ErrorMessage = ex.Message;
            }


            return serviceResponse;
        }

        private string GetResponseMessage(HttpResponseMessage response)
        {
            switch(response.StatusCode)
            {
                case (System.Net.HttpStatusCode.Forbidden):
                    return "User is Forbidden";
                case (System.Net.HttpStatusCode.Unauthorized):
                    return "User is Unauthorized";
                case (System.Net.HttpStatusCode.GatewayTimeout):
                    return "Request timed out. Please try again";
                case (System.Net.HttpStatusCode.TooManyRequests):
                    return "Too many requests have been made. Please wait a while and try again";
                default:
                    return $"Error: {response.StatusCode}. Error retrieving data from the API";
            }
        }

        //Method to get the station based on the location
        //I could not find the API on Weather.Gov for finding the coordinates, which are then used to grab the station.
        //With more time, a good interim approach would be to use another API to grab the coordinates of the provided location
        //then use a mathematical approach to narrow down the specific station that is closest to the station.
        //I will start working on this approach if I have time
        private string GetStationId(string location)
        {
            var random = new Random();
            var stationIds = new List<string>{
                "0007W",
                "047PG",
                "0579W",
                "059PG",
                "069PG"};
            return stationIds[random.Next(stationIds.Count)];
        }

        private List<DailyTemperature> MapResponseToDailyTemperature(JObject obj)
        {
            var features = (JArray)obj["features"];


            Dictionary<DateTime, List<decimal>> readings = new();

            List<DailyTemperature> dailyTemperatures = new();
            foreach (var item in features)
            {
                var tempToken = item["properties"]["temperature"]["value"];
                var dateToken = item["properties"]["timestamp"];

                if (tempToken.Type == JTokenType.Null || dateToken.Type == JTokenType.Null)
                {
                    continue;
                }

                var temp = tempToken.Value<decimal>();
                var date = dateToken.Value<DateTime>();
                var formattedDate = new DateTime(date.Year, date.Month, date.Day);

                if (readings.ContainsKey(formattedDate))
                {
                    var list = readings[formattedDate];
                    list.Add(temp);
                    readings[formattedDate] = list;
                }
                else
                {
                    readings[formattedDate] = new List<decimal> { temp };
                }
            }


            //Grab the high and the low temp for each day
            foreach (var key in readings.Keys)
            {
                var values = readings[key];
                dailyTemperatures.Add(
                    new DailyTemperature
                    {
                        Date = key,
                        HighTemperature = values.Max(),
                        LowTemperature = values.Min()
                    });
            }
            return dailyTemperatures;
        }

    }
}
