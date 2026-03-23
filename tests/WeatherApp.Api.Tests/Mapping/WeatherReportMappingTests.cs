using WeatherApp.Api.Controllers;
using WeatherApp.Api.Mapping;
using WeatherApp.Domain.Models;
using Xunit;

namespace WeatherApp.Api.Tests;

public sealed class WeatherReportMappingTests
{
    [Fact]
    public void Map_ShouldConvertDomainModelToDto()
    {
        var mapperType = typeof(WeatherController).Assembly.GetType("WeatherApp.Api.Mapping.WeatherReportMapping", throwOnError: true)!;
        var mapper = (IWeatherReportDtoMapper)Activator.CreateInstance(mapperType, nonPublic: true)!;
        var report = new WeatherReport
        {
            CityName = "Moscow",
            Current = new CurrentWeather
            {
                TemperatureC = 7m,
                FeelsLikeC = 4m,
                ConditionText = "Cloudy",
                ConditionIconUrl = "//cdn.weather/current.png",
                Humidity = 80,
                WindKph = 13m,
                PressureMb = 755m,
                LastUpdatedLocal = new DateTime(2026, 3, 23, 14, 0, 0, DateTimeKind.Unspecified),
            },
            HourlyForecast =
            [
                new HourlyForecastItem
                {
                    LocalTime = new DateTime(2026, 3, 23, 15, 0, 0, DateTimeKind.Unspecified),
                    TemperatureC = 8m,
                    ConditionText = "Rain",
                    ConditionIconUrl = "//cdn.weather/hour.png",
                    ChanceOfRain = 40,
                },
            ],
            DailyForecast =
            [
                new DailyForecastItem
                {
                    Date = new DateTime(2026, 3, 24),
                    MinTemperatureC = 3m,
                    MaxTemperatureC = 9m,
                    ConditionText = "Cloudy",
                    ConditionIconUrl = "//cdn.weather/day.png",
                },
            ],
        };

        var dto = mapper.Map(report);

        Assert.Equal("Moscow", dto.CityName);
        Assert.Equal(7m, dto.Current.TemperatureC);
        Assert.Equal(4m, dto.Current.FeelsLikeC);
        Assert.Equal("Cloudy", dto.Current.ConditionText);
        Assert.Equal("//cdn.weather/current.png", dto.Current.ConditionIconUrl);
        Assert.Equal(80, dto.Current.Humidity);
        Assert.Equal(13m, dto.Current.WindKph);
        Assert.Equal(755m, dto.Current.PressureMb);
        Assert.Equal(new DateTime(2026, 3, 23, 14, 0, 0, DateTimeKind.Unspecified), dto.Current.LastUpdatedLocal);

        var hourly = Assert.Single(dto.HourlyForecast);
        Assert.Equal(new DateTime(2026, 3, 23, 15, 0, 0, DateTimeKind.Unspecified), hourly.LocalTime);
        Assert.Equal(8m, hourly.TemperatureC);
        Assert.Equal("Rain", hourly.ConditionText);
        Assert.Equal("//cdn.weather/hour.png", hourly.ConditionIconUrl);
        Assert.Equal(40, hourly.ChanceOfRain);

        var daily = Assert.Single(dto.DailyForecast);
        Assert.Equal(new DateTime(2026, 3, 24), daily.Date);
        Assert.Equal(3m, daily.MinTemperatureC);
        Assert.Equal(9m, daily.MaxTemperatureC);
        Assert.Equal("Cloudy", daily.ConditionText);
        Assert.Equal("//cdn.weather/day.png", daily.ConditionIconUrl);
    }
}
