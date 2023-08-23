using System.Net;
using System.Text.Json;

namespace ConsoleApp5
{
    public class WeatherModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public WeatherResultRoot WeatherResult { get; set; }
        public string Error { get; set; }

        public void PrintResult()
        {
            PrintGeneralInfo();
            if (this.Error == null)
            {
                PrintCoordInfo();
                PrintWeatherInfo();
                PrintMainInfo();
                PrintVisibilityAndWindInfo();
                PrintAdditionalInfo();
                PrintSysInfo();
            }
        }
        private void PrintGeneralInfo()
        {
            Console.WriteLine($"Error: {this.Error}");
            Console.WriteLine($"Message {this.Message}");
            Console.WriteLine($"StatusCode: {this.StatusCode}");
        }

        private void PrintCoordInfo()
        {
            Console.WriteLine($"coord.lon: {this.WeatherResult.coord.lon}");
            Console.WriteLine($"coord.lat: {this.WeatherResult.coord.lat}");
        }

        private void PrintWeatherInfo()
        {
            if (this.WeatherResult.weather != null && this.WeatherResult.weather.Any())
            {
                Console.WriteLine($"weather.id: {this.WeatherResult.weather[0].id}");
                Console.WriteLine($"weather.main: {this.WeatherResult.weather[0].main}");
                Console.WriteLine($"weather.description: {this.WeatherResult.weather[0].description}");
                Console.WriteLine($"weather.icon: {this.WeatherResult.weather[0].icon}");
            }
        }

        private void PrintMainInfo()
        {
            Console.WriteLine($"base: {this.WeatherResult.@base}");
            Console.WriteLine($"main.temp: {this.WeatherResult.main.temp}");
            Console.WriteLine($"main.feels_like: {this.WeatherResult.main.feels_like}");
            Console.WriteLine($"main.temp_min: {this.WeatherResult.main.temp_min}");
            Console.WriteLine($"main.temp_max: {this.WeatherResult.main.temp_max}");
            Console.WriteLine($"main.pressure: {this.WeatherResult.main.pressure}");
            Console.WriteLine($"main.humidity: {this.WeatherResult.main.humidity}");
        }

        private void PrintVisibilityAndWindInfo()
        {
            Console.WriteLine($"visibility: {this.WeatherResult.visibility}");
            Console.WriteLine($"wind.speed: {this.WeatherResult.wind.speed}");
            Console.WriteLine($"wind.deg: {this.WeatherResult.wind.deg}");
            Console.WriteLine($"wind.gust: {this.WeatherResult.wind.gust}");
            Console.WriteLine($"rain.oneHour: {this.WeatherResult.rain?._1h}");
            Console.WriteLine($"clouds.all: {this.WeatherResult.clouds.all}");
        }

        private void PrintAdditionalInfo()
        {
            Console.WriteLine($"dt: {this.WeatherResult.dt}");
            Console.WriteLine($"timezone: {this.WeatherResult.timezone}");
            Console.WriteLine($"id: {this.WeatherResult.id}");
            Console.WriteLine($"name: {this.WeatherResult.name}");
            Console.WriteLine($"cod: {this.WeatherResult.cod}");
        }

        private void PrintSysInfo()
        {
            Console.WriteLine($"sys.type: {this.WeatherResult.sys.type}");
            Console.WriteLine($"sys.id: {this.WeatherResult.sys.id}");
            Console.WriteLine($"sys.country: {this.WeatherResult.sys.country}");
            Console.WriteLine($"sys.sunrise: {this.WeatherResult.sys.sunrise}");
            Console.WriteLine($"sys.sunset: {this.WeatherResult.sys.sunset}");
        }
    }

    public class ClientWeather
    {

        private readonly string key;
        private readonly HttpClient client;
        private WeatherModel Weathermodel;

        public ClientWeather(string key)
        {
            this.client = new HttpClient();
            this.key = key;
        }

        private async Task<HttpResponseMessage> GetWeatherHttpResponseForCity(string city)
        {
            string apiUrl = BuildApiUrl(city);
            return await client.GetAsync(apiUrl);
        }

        private string BuildApiUrl(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={this.key}";
        }

        private async Task<WeatherModel> ProcessHttpResponse(HttpResponseMessage response)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                WeatherResultRoot weatherData = JsonSerializer.Deserialize<WeatherResultRoot>(jsonResponse);
                return new WeatherModel
                {
                    Message = "Data retrieved successfully",
                    StatusCode = response.StatusCode,
                    WeatherResult = weatherData,
                    Error = null
                };
            }
            else
            {
                return new WeatherModel
                {
                    Message = "Error retrieving data",
                    StatusCode = response.StatusCode,
                    WeatherResult = null,
                    Error = jsonResponse
                };
            }
        }

        private WeatherModel CreateErrorWeatherModel(string errorMessage)
        {
            return new WeatherModel
            {
                Message = "Error retrieving data",
                StatusCode = HttpStatusCode.InternalServerError,
                WeatherResult = null,
                Error = errorMessage
            };
        }

        public async Task<WeatherModel> Get()
        {
            string apiUrl = BuildApiUrl("Mykolaiv");

            try
            {
                HttpResponseMessage response = await GetWeatherHttpResponseForCity("Mykolaiv");
                return await ProcessHttpResponse(response);
            }
            catch (Exception ex)
            {
                return CreateErrorWeatherModel(ex.Message);
            }
        }

        public async Task<WeatherModel> Post(string city)
        {
            string apiUrl = BuildApiUrl(city);
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                return await ProcessHttpResponse(response);
            }
            catch (Exception ex)
            {
                return CreateErrorWeatherModel(ex.Message);
            }
        }
    }
}
