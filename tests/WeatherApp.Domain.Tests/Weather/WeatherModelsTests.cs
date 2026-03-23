using WeatherApp.Domain.Models;
using Xunit;

namespace WeatherApp.Domain.Tests;

public sealed class CurrentWeatherTests
{
    [Fact]
    public void Constructor_ShouldInitializeDefaultValues()
    {
        var model = new CurrentWeather();

        Assert.Equal(0m, model.TemperatureC);
        Assert.Equal(0m, model.FeelsLikeC);
        Assert.Equal(string.Empty, model.ConditionText);
        Assert.Equal(string.Empty, model.ConditionIconUrl);
        Assert.Equal(0, model.Humidity);
        Assert.Equal(0m, model.WindKph);
        Assert.Equal(0m, model.PressureMb);
        Assert.Equal(default, model.LastUpdatedLocal);
    }

    [Fact]
    public void Properties_ShouldStoreAssignedValues()
    {
        var updatedAt = new DateTime(2026, 3, 23, 12, 30, 0, DateTimeKind.Unspecified);
        var model = new CurrentWeather
        {
            TemperatureC = 8.5m,
            FeelsLikeC = 6.2m,
            ConditionText = "Cloudy",
            ConditionIconUrl = "//cdn.weather/current.png",
            Humidity = 72,
            WindKph = 18.4m,
            PressureMb = 1001.7m,
            LastUpdatedLocal = updatedAt,
        };

        Assert.Equal(8.5m, model.TemperatureC);
        Assert.Equal(6.2m, model.FeelsLikeC);
        Assert.Equal("Cloudy", model.ConditionText);
        Assert.Equal("//cdn.weather/current.png", model.ConditionIconUrl);
        Assert.Equal(72, model.Humidity);
        Assert.Equal(18.4m, model.WindKph);
        Assert.Equal(1001.7m, model.PressureMb);
        Assert.Equal(updatedAt, model.LastUpdatedLocal);
    }
}

public sealed class HourlyForecastItemTests
{
    [Fact]
    public void Constructor_ShouldInitializeDefaultValues()
    {
        var model = new HourlyForecastItem();

        Assert.Equal(default, model.LocalTime);
        Assert.Equal(0m, model.TemperatureC);
        Assert.Equal(string.Empty, model.ConditionText);
        Assert.Equal(string.Empty, model.ConditionIconUrl);
        Assert.Equal(0, model.ChanceOfRain);
    }

    [Fact]
    public void Properties_ShouldStoreAssignedValues()
    {
        var localTime = new DateTime(2026, 3, 23, 14, 0, 0, DateTimeKind.Unspecified);
        var model = new HourlyForecastItem
        {
            LocalTime = localTime,
            TemperatureC = 7m,
            ConditionText = "Light rain",
            ConditionIconUrl = "//cdn.weather/hour.png",
            ChanceOfRain = 65,
        };

        Assert.Equal(localTime, model.LocalTime);
        Assert.Equal(7m, model.TemperatureC);
        Assert.Equal("Light rain", model.ConditionText);
        Assert.Equal("//cdn.weather/hour.png", model.ConditionIconUrl);
        Assert.Equal(65, model.ChanceOfRain);
    }
}

public sealed class DailyForecastItemTests
{
    [Fact]
    public void Constructor_ShouldInitializeDefaultValues()
    {
        var model = new DailyForecastItem();

        Assert.Equal(default, model.Date);
        Assert.Equal(0m, model.MinTemperatureC);
        Assert.Equal(0m, model.MaxTemperatureC);
        Assert.Equal(string.Empty, model.ConditionText);
        Assert.Equal(string.Empty, model.ConditionIconUrl);
    }

    [Fact]
    public void Properties_ShouldStoreAssignedValues()
    {
        var date = new DateTime(2026, 3, 24);
        var model = new DailyForecastItem
        {
            Date = date,
            MinTemperatureC = 2.4m,
            MaxTemperatureC = 9.1m,
            ConditionText = "Sunny",
            ConditionIconUrl = "//cdn.weather/day.png",
        };

        Assert.Equal(date, model.Date);
        Assert.Equal(2.4m, model.MinTemperatureC);
        Assert.Equal(9.1m, model.MaxTemperatureC);
        Assert.Equal("Sunny", model.ConditionText);
        Assert.Equal("//cdn.weather/day.png", model.ConditionIconUrl);
    }
}

public sealed class WeatherReportTests
{
    [Fact]
    public void Constructor_ShouldInitializeDefaultValues()
    {
        var model = new WeatherReport();

        Assert.Equal(string.Empty, model.CityName);
        Assert.NotNull(model.Current);
        Assert.Empty(model.HourlyForecast);
        Assert.Empty(model.DailyForecast);
    }

    [Fact]
    public void Properties_ShouldStoreAssignedValues()
    {
        var current = new CurrentWeather { TemperatureC = 12m };
        IReadOnlyCollection<HourlyForecastItem> hourly = [new HourlyForecastItem { TemperatureC = 13m }];
        IReadOnlyCollection<DailyForecastItem> daily = [new DailyForecastItem { MaxTemperatureC = 15m }];
        var model = new WeatherReport
        {
            CityName = "Moscow",
            Current = current,
            HourlyForecast = hourly,
            DailyForecast = daily,
        };

        Assert.Equal("Moscow", model.CityName);
        Assert.Same(current, model.Current);
        Assert.Same(hourly, model.HourlyForecast);
        Assert.Same(daily, model.DailyForecast);
    }
}
