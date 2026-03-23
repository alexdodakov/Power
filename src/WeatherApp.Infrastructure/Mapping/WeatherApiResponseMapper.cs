using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Dtos;
using WeatherApp.Infrastructure.Parsing;

namespace WeatherApp.Infrastructure.Mapping;

public sealed class WeatherApiResponseMapper : IWeatherApiResponseMapper
{
    private readonly IDateTimeParser _dateTimeParser;
    private readonly IDateParser _dateParser;

    public WeatherApiResponseMapper(IDateTimeParser dateTimeParser, IDateParser dateParser)
    {
        _dateTimeParser = dateTimeParser;
        _dateParser = dateParser;
    }

    public WeatherApiPayload ToPayload(ForecastWeatherResponseDto response)
    {
        var hourly = response.Forecast.Forecastday
            .SelectMany(day => day.Hour)
            .Select(x => new WeatherApiHourlyForecastPayload
            {
                LocalTime = _dateTimeParser.ParseDateTime(x.Time),
                TemperatureC = x.TempC,
                ConditionText = x.Condition.Text,
                ConditionIconUrl = x.Condition.Icon,
                ChanceOfRain = x.ChanceOfRain,
            })
            .ToArray();

        var daily = response.Forecast.Forecastday
            .Select(day => new WeatherApiDailyForecastPayload
            {
                Date = _dateParser.ParseDate(day.Date),
                MinTemperatureC = day.Day.MinTempC,
                MaxTemperatureC = day.Day.MaxTempC,
                ConditionText = day.Day.Condition.Text,
                ConditionIconUrl = day.Day.Condition.Icon,
            })
            .ToArray();

        return new WeatherApiPayload
        {
            CityName = response.Location.Name,
            TemperatureC = response.Current.TempC,
            FeelsLikeC = response.Current.FeelsLikeC,
            ConditionText = response.Current.Condition.Text,
            ConditionIconUrl = response.Current.Condition.Icon,
            Humidity = response.Current.Humidity,
            WindKph = response.Current.WindKph,
            PressureMb = response.Current.PressureMb,
            LastUpdatedLocal = _dateTimeParser.ParseDateTime(response.Current.LastUpdated),
            HourlyForecast = hourly,
            DailyForecast = daily,
        };
    }
}
