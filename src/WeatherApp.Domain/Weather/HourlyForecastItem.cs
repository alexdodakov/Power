namespace WeatherApp.Domain.Models;

public sealed class HourlyForecastItem
{
    public DateTime LocalTime { get; set; }

    public decimal TemperatureC { get; set; }

    public string ConditionText { get; set; } = string.Empty;

    public string ConditionIconUrl { get; set; } = string.Empty;

    public int ChanceOfRain { get; set; }
}
