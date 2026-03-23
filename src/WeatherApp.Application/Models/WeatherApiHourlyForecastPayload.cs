namespace WeatherApp.Application.Models;

public sealed class WeatherApiHourlyForecastPayload
{
    public DateTime LocalTime { get; init; }

    public decimal TemperatureC { get; init; }

    public string ConditionText { get; init; } = string.Empty;

    public string ConditionIconUrl { get; init; } = string.Empty;

    public int ChanceOfRain { get; init; }
}
