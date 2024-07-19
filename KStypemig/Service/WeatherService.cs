using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public async Task<WeatherData> GetWeatherDataAsync(string city, string apiKey)
    {
        string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<WeatherData>(content);
    }
    public async Task<List<WeatherData>> GetWeatherDataForCitiesAsync(List<string> cities, string apiKey)
    {
        var weatherDataList = new List<WeatherData>();

        foreach (var city in cities)
        {
            var data = await GetWeatherDataAsync(city, apiKey);
            weatherDataList.Add(data);
        }

        return weatherDataList;
    }
}

public class WeatherData
{
    public Main Main { get; set; }
    public string Name { get; set; }
}

public class Main
{
    public double Temp { get; set; }
    public double Humidity { get; set; }
}
