namespace WeatherApp.Application.Models;

public sealed class WeatherApiDailyForecastPayload
{
    public DateTime Date { get; init; }

    public decimal MinTemperatureC { get; init; }

    public decimal MaxTemperatureC { get; init; }

    public string ConditionText { get; init; } = string.Empty;

    public string ConditionIconUrl { get; init; } = string.Empty;
}
