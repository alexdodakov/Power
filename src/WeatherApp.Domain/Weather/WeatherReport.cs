namespace WeatherApp.Domain.Models;

public sealed class WeatherReport
{
    public string CityName { get; set; } = string.Empty;

    public CurrentWeather Current { get; set; } = new CurrentWeather();

    public IReadOnlyCollection<HourlyForecastItem> HourlyForecast { get; set; } = Array.Empty<HourlyForecastItem>();

    public IReadOnlyCollection<DailyForecastItem> DailyForecast { get; set; } = Array.Empty<DailyForecastItem>();
}
