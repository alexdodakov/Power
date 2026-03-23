using WeatherApp.Application.Mapping;
using WeatherApp.Application.Models;
using Xunit;

namespace WeatherApp.Application.Tests;

public sealed class WeatherConditionTextTranslatorTests
{
    private readonly WeatherConditionTextTranslator _translator = new();

    [Fact]
    public void Translate_WhenConditionExists_ShouldReturnRussianTranslation()
    {
        var result = _translator.Translate("Sunny");

        Assert.Equal("Солнечно", result);
    }

    [Fact]
    public void Translate_WhenConditionHasDifferentCaseAndExtraSpaces_ShouldNormalizeAndTranslate()
    {
        var result = _translator.Translate("  partly    cloudy  ");

        Assert.Equal("Переменная облачность", result);
    }

    [Fact]
    public void Translate_WhenConditionIsUnknown_ShouldReturnNormalizedText()
    {
        var result = _translator.Translate("  Custom    condition ");

        Assert.Equal("Custom condition", result);
    }

    [Fact]
    public void Translate_WhenTextIsNull_ShouldReturnEmptyString()
    {
        var result = _translator.Translate(null!);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Translate_WhenTextIsWhitespace_ShouldReturnOriginalWhitespace()
    {
        var result = _translator.Translate("   ");

        Assert.Equal("   ", result);
    }
}

public sealed class WeatherReportMapperTests
{
    [Fact]
    public void Map_ShouldMapCurrentHourlyAndDailyData()
    {
        var translator = new PrefixTranslator();
        var mapper = new WeatherReportMapper(translator);
        var payload = new WeatherApiPayload
        {
            CityName = "Moscow",
            TemperatureC = 8m,
            FeelsLikeC = 6m,
            ConditionText = "Cloudy",
            ConditionIconUrl = "//cdn.weather/current.png",
            Humidity = 80,
            WindKph = 15m,
            PressureMb = 1001m,
            LastUpdatedLocal = new DateTime(2026, 3, 23, 10, 0, 0, DateTimeKind.Unspecified),
            HourlyForecast =
            [
                new WeatherApiHourlyForecastPayload
                {
                    LocalTime = new DateTime(2026, 3, 23, 11, 0, 0, DateTimeKind.Unspecified),
                    TemperatureC = 9m,
                    ConditionText = "Light rain",
                    ConditionIconUrl = "//cdn.weather/hour.png",
                    ChanceOfRain = 35,
                },
            ],
            DailyForecast =
            [
                new WeatherApiDailyForecastPayload
                {
                    Date = new DateTime(2026, 3, 24),
                    MinTemperatureC = 4m,
                    MaxTemperatureC = 10m,
                    ConditionText = "Sunny",
                    ConditionIconUrl = "//cdn.weather/day.png",
                },
            ],
        };

        var report = mapper.Map(payload);

        Assert.Equal("Moscow", report.CityName);
        Assert.Equal(8m, report.Current.TemperatureC);
        Assert.Equal(6m, report.Current.FeelsLikeC);
        Assert.Equal("RU:Cloudy", report.Current.ConditionText);
        Assert.Equal("//cdn.weather/current.png", report.Current.ConditionIconUrl);
        Assert.Equal(80, report.Current.Humidity);
        Assert.Equal(15m, report.Current.WindKph);
        Assert.Equal(751m, report.Current.PressureMb);
        Assert.Equal(new DateTime(2026, 3, 23, 10, 0, 0, DateTimeKind.Unspecified), report.Current.LastUpdatedLocal);

        var hourly = Assert.Single(report.HourlyForecast);
        Assert.Equal(new DateTime(2026, 3, 23, 11, 0, 0, DateTimeKind.Unspecified), hourly.LocalTime);
        Assert.Equal(9m, hourly.TemperatureC);
        Assert.Equal("RU:Light rain", hourly.ConditionText);
        Assert.Equal("//cdn.weather/hour.png", hourly.ConditionIconUrl);
        Assert.Equal(35, hourly.ChanceOfRain);

        var daily = Assert.Single(report.DailyForecast);
        Assert.Equal(new DateTime(2026, 3, 24), daily.Date);
        Assert.Equal(4m, daily.MinTemperatureC);
        Assert.Equal(10m, daily.MaxTemperatureC);
        Assert.Equal("RU:Sunny", daily.ConditionText);
        Assert.Equal("//cdn.weather/day.png", daily.ConditionIconUrl);

        Assert.Equal(new[] { "Cloudy", "Light rain", "Sunny" }, translator.Received);
    }
}
