using System;
using System.Linq;
using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Application.Mapping;

public sealed class WeatherReportMapper : IWeatherReportMapper
{
    private const decimal MillibarsToMillimetersOfMercury = 0.750061683m;
    private readonly IWeatherConditionTextTranslator _conditionTranslator;

    public WeatherReportMapper(IWeatherConditionTextTranslator conditionTranslator)
    {
        _conditionTranslator = conditionTranslator;
    }

    public WeatherReport Map(WeatherApiPayload payload)
    {
        return new WeatherReport
        {
            CityName = payload.CityName,
            Current = new CurrentWeather
            {
                TemperatureC = payload.TemperatureC,
                FeelsLikeC = payload.FeelsLikeC,
                ConditionText = _conditionTranslator.Translate(payload.ConditionText),
                ConditionIconUrl = payload.ConditionIconUrl,
                Humidity = payload.Humidity,
                WindKph = payload.WindKph,
                PressureMb = decimal.Round(
                    payload.PressureMb * MillibarsToMillimetersOfMercury,
                    0,
                    MidpointRounding.AwayFromZero),
                LastUpdatedLocal = payload.LastUpdatedLocal,
            },
            HourlyForecast = payload.HourlyForecast
                .Select(item => new HourlyForecastItem
                {
                    LocalTime = item.LocalTime,
                    TemperatureC = item.TemperatureC,
                    ConditionText = _conditionTranslator.Translate(item.ConditionText),
                    ConditionIconUrl = item.ConditionIconUrl,
                    ChanceOfRain = item.ChanceOfRain,
                })
                .ToArray(),
            DailyForecast = payload.DailyForecast
                .Select(item => new DailyForecastItem
                {
                    Date = item.Date,
                    MinTemperatureC = item.MinTemperatureC,
                    MaxTemperatureC = item.MaxTemperatureC,
                    ConditionText = _conditionTranslator.Translate(item.ConditionText),
                    ConditionIconUrl = item.ConditionIconUrl,
                })
                .ToArray(),
        };
    }
}
