using FluentValidation;
using WeatherApp.Application.Models;
using WeatherApp.Application.Services;
using WeatherApp.Application.Validation;
using WeatherApp.Domain.Models;
using Xunit;

namespace WeatherApp.Application.Tests;

public sealed class WeatherFacadeTests
{
    [Fact]
    public async Task GetWeatherReportAsync_WhenQueryIsInvalid_ShouldThrowValidationException()
    {
        var apiClient = new FakeWeatherApiClient();
        var mapper = new FakeWeatherReportMapper();
        var facade = new WeatherFacade(apiClient, new GetWeatherReportQueryValidator(), mapper);
        var query = new GetWeatherReportQuery
        {
            Latitude = 500m,
            Longitude = 37.6176m,
            Days = 3,
        };

        await Assert.ThrowsAsync<ValidationException>(() => facade.GetWeatherReportAsync(query));
        Assert.False(apiClient.WasCalled);
    }

    [Fact]
    public async Task GetWeatherReportAsync_WhenQueryIsValid_ShouldReturnMappedWeatherReport()
    {
        var query = new GetWeatherReportQuery
        {
            Latitude = 55.7558m,
            Longitude = 37.6176m,
            Days = 3,
        };

        var payload = new WeatherApiPayload
        {
            CityName = "Moscow",
            TemperatureC = 7m,
            FeelsLikeC = 4m,
            ConditionText = "Cloudy",
            ConditionIconUrl = "//cdn.weather/icon.png",
            Humidity = 78,
            WindKph = 18m,
            PressureMb = 1002m,
            LastUpdatedLocal = new DateTime(2026, 3, 23, 14, 0, 0, DateTimeKind.Unspecified),
            HourlyForecast =
            [
                new WeatherApiHourlyForecastPayload
                {
                    LocalTime = new DateTime(2026, 3, 23, 15, 0, 0, DateTimeKind.Unspecified),
                    TemperatureC = 7m,
                    ConditionText = "Cloudy",
                    ConditionIconUrl = "//cdn.weather/hour.png",
                    ChanceOfRain = 15,
                },
            ],
            DailyForecast =
            [
                new WeatherApiDailyForecastPayload
                {
                    Date = new DateTime(2026, 3, 23),
                    MinTemperatureC = 3m,
                    MaxTemperatureC = 8m,
                    ConditionText = "Cloudy",
                    ConditionIconUrl = "//cdn.weather/day.png",
                },
            ],
        };

        var apiClient = new FakeWeatherApiClient { Payload = payload };
        var mapper = new FakeWeatherReportMapper
        {
            Result = new WeatherReport
            {
                CityName = payload.CityName,
                Current = new CurrentWeather(),
                HourlyForecast = Array.Empty<HourlyForecastItem>(),
                DailyForecast = Array.Empty<DailyForecastItem>(),
            }
        };
        var facade = new WeatherFacade(apiClient, new GetWeatherReportQueryValidator(), mapper);

        var report = await facade.GetWeatherReportAsync(query);

        Assert.True(apiClient.WasCalled);
        Assert.Equal(query, apiClient.ReceivedQuery);
        Assert.Equal(payload.CityName, report.CityName);
        Assert.Same(mapper.Result, report);
    }
}
