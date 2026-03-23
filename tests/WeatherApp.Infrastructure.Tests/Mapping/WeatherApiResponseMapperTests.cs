using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Dtos;
using WeatherApp.Infrastructure.Mapping;
using Xunit;

namespace WeatherApp.Infrastructure.Tests;

public sealed class WeatherApiResponseMapperTests
{
    [Fact]
    public void ToPayload_ShouldMapAllFieldsAndUseParsers()
    {
        var dateTimeParser = new FakeDateTimeParser();
        var dateParser = new FakeDateParser();
        var mapper = new WeatherApiResponseMapper(dateTimeParser, dateParser);
        var response = new ForecastWeatherResponseDto
        {
            Location = new LocationDto { Name = "Moscow", Region = "Moscow" },
            Current = new CurrentDto
            {
                TempC = 8m,
                FeelsLikeC = 6m,
                Condition = new ConditionDto { Text = "Cloudy", Icon = "//cdn.weather/current.png" },
                Humidity = 77,
                WindKph = 15m,
                PressureMb = 1000m,
                LastUpdated = "2026-03-23 10:00",
            },
            Forecast = new ForecastDto
            {
                Forecastday =
                [
                    new ForecastDayDto
                    {
                        Date = "2026-03-23",
                        Day = new DayDto
                        {
                            MinTempC = 4m,
                            MaxTempC = 9m,
                            Condition = new ConditionDto { Text = "Rain", Icon = "//cdn.weather/day-1.png" },
                        },
                        Hour =
                        [
                            new HourDto
                            {
                                Time = "2026-03-23 11:00",
                                TempC = 9m,
                                Condition = new ConditionDto { Text = "Light rain", Icon = "//cdn.weather/hour-1.png" },
                                ChanceOfRain = 40,
                            },
                        ],
                    },
                    new ForecastDayDto
                    {
                        Date = "2026-03-24",
                        Day = new DayDto
                        {
                            MinTempC = 3m,
                            MaxTempC = 11m,
                            Condition = new ConditionDto { Text = "Sunny", Icon = "//cdn.weather/day-2.png" },
                        },
                        Hour =
                        [
                            new HourDto
                            {
                                Time = "2026-03-24 12:00",
                                TempC = 10m,
                                Condition = new ConditionDto { Text = "Sunny", Icon = "//cdn.weather/hour-2.png" },
                                ChanceOfRain = 5,
                            },
                        ],
                    },
                ],
            },
        };

        var payload = mapper.ToPayload(response);

        Assert.Equal("Moscow", payload.CityName);
        Assert.Equal(8m, payload.TemperatureC);
        Assert.Equal(6m, payload.FeelsLikeC);
        Assert.Equal("Cloudy", payload.ConditionText);
        Assert.Equal("//cdn.weather/current.png", payload.ConditionIconUrl);
        Assert.Equal(77, payload.Humidity);
        Assert.Equal(15m, payload.WindKph);
        Assert.Equal(1000m, payload.PressureMb);

        Assert.Equal(new DateTime(2030, 1, 1, 8, 0, 0, DateTimeKind.Unspecified), payload.LastUpdatedLocal);

        var hourly = payload.HourlyForecast.ToArray();
        Assert.Equal(2, hourly.Length);
        Assert.Equal(new DateTime(2030, 1, 1, 8, 0, 0, DateTimeKind.Unspecified), hourly[0].LocalTime);
        Assert.Equal(new DateTime(2030, 1, 1, 9, 0, 0, DateTimeKind.Unspecified), hourly[1].LocalTime);

        var daily = payload.DailyForecast.ToArray();
        Assert.Equal(2, daily.Length);
        Assert.Equal(new DateTime(2030, 1, 2), daily[0].Date);
        Assert.Equal(new DateTime(2030, 1, 3), daily[1].Date);

        Assert.Equal(
            new[] { "2026-03-23 11:00", "2026-03-24 12:00", "2026-03-23 10:00" },
            dateTimeParser.ReceivedValues);
        Assert.Equal(new[] { "2026-03-23", "2026-03-24" }, dateParser.ReceivedValues);
    }
}
