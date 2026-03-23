namespace WeatherApp.Api.Models;

public sealed record CurrentWeatherDto(
    decimal TemperatureC,
    decimal FeelsLikeC,
    string ConditionText,
    string ConditionIconUrl,
    int Humidity,
    decimal WindKph,
    decimal PressureMb,
    DateTime LastUpdatedLocal);

public sealed record HourlyForecastDto(
    DateTime LocalTime,
    decimal TemperatureC,
    string ConditionText,
    string ConditionIconUrl,
    int ChanceOfRain);

public sealed record DailyForecastDto(
    DateTime Date,
    decimal MinTemperatureC,
    decimal MaxTemperatureC,
    string ConditionText,
    string ConditionIconUrl);

public sealed record WeatherPageDto(
    string CityName,
    CurrentWeatherDto Current,
    IReadOnlyCollection<HourlyForecastDto> HourlyForecast,
    IReadOnlyCollection<DailyForecastDto> DailyForecast);
