using WeatherApp.Api.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Api.Mapping;

internal sealed class WeatherReportMapping : IWeatherReportDtoMapper
{
    public WeatherPageDto Map(WeatherReport report)
    {
        var current = report.Current;
        return new WeatherPageDto(
            report.CityName,
            new CurrentWeatherDto(
                current.TemperatureC,
                current.FeelsLikeC,
                current.ConditionText,
                current.ConditionIconUrl,
                current.Humidity,
                current.WindKph,
                current.PressureMb,
                current.LastUpdatedLocal),
            report.HourlyForecast
                .Select(item => new HourlyForecastDto(
                    item.LocalTime,
                    item.TemperatureC,
                    item.ConditionText,
                    item.ConditionIconUrl,
                    item.ChanceOfRain))
                .ToArray(),
            report.DailyForecast
                .Select(item => new DailyForecastDto(
                    item.Date,
                    item.MinTemperatureC,
                    item.MaxTemperatureC,
                    item.ConditionText,
                    item.ConditionIconUrl))
                .ToArray());
    }
}
