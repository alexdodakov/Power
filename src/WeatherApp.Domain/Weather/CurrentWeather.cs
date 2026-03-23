using System;

namespace WeatherApp.Domain.Models;

public sealed class CurrentWeather
{
    public decimal TemperatureC { get; set; }

    public decimal FeelsLikeC { get; set; }

    public string ConditionText { get; set; } = string.Empty;

    public string ConditionIconUrl { get; set; } = string.Empty;

    public int Humidity { get; set; }

    public decimal WindKph { get; set; }

    public decimal PressureMb { get; set; }

    public DateTime LastUpdatedLocal { get; set; }
}
