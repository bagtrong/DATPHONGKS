using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

public class WeatherController : Controller
{
    private readonly WeatherService _weatherService = new WeatherService();
    private readonly string _apiKey = "99440e64324ee7fa484eb254f266b25b";  // Thay YOUR_API_KEY bằng API key của bạn từ OpenWeatherMap

   
    public async Task<ActionResult> Index()
    {
        var cities = new List<string> { "Ha Noi","VietNam" };
        var weatherDataList = await _weatherService.GetWeatherDataForCitiesAsync(cities, _apiKey);
        return View(weatherDataList);
    }
}
