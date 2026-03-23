namespace WeatherApp.Application.Models;

public sealed class WeatherApiPayload
{
    public string CityName { get; init; } = string.Empty;

    public decimal TemperatureC { get; init; }

    public decimal FeelsLikeC { get; init; }

    public string ConditionText { get; init; } = string.Empty;

    public string ConditionIconUrl { get; init; } = string.Empty;

    public int Humidity { get; init; }

    public decimal WindKph { get; init; }

    public decimal PressureMb { get; init; }

    public DateTime LastUpdatedLocal { get; init; }

    public IReadOnlyCollection<WeatherApiHourlyForecastPayload> HourlyForecast { get; init; } = Array.Empty<WeatherApiHourlyForecastPayload>();

    public IReadOnlyCollection<WeatherApiDailyForecastPayload> DailyForecast { get; init; } = Array.Empty<WeatherApiDailyForecastPayload>();
}
