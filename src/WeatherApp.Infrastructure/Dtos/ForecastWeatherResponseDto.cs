using System.Text.Json.Serialization;

namespace WeatherApp.Infrastructure.Dtos;

public sealed class ForecastWeatherResponseDto
{
    [JsonPropertyName("location")]
    public LocationDto Location { get; set; } = new();

    [JsonPropertyName("current")]
    public CurrentDto Current { get; set; } = new();

    [JsonPropertyName("forecast")]
    public ForecastDto Forecast { get; set; } = new();
}

public sealed class LocationDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
}

public sealed class ForecastDto
{
    [JsonPropertyName("forecastday")]
    public IReadOnlyCollection<ForecastDayDto> Forecastday { get; set; } = Array.Empty<ForecastDayDto>();
}

public sealed class ForecastDayDto
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("day")]
    public DayDto Day { get; set; } = new();

    [JsonPropertyName("hour")]
    public IReadOnlyCollection<HourDto> Hour { get; set; } = Array.Empty<HourDto>();
}

public sealed class DayDto
{
    [JsonPropertyName("mintemp_c")]
    public decimal MinTempC { get; set; }

    [JsonPropertyName("maxtemp_c")]
    public decimal MaxTempC { get; set; }

    [JsonPropertyName("condition")]
    public ConditionDto Condition { get; set; } = new();
}

public sealed class HourDto
{
    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("condition")]
    public ConditionDto Condition { get; set; } = new();

    [JsonPropertyName("chance_of_rain")]
    public int ChanceOfRain { get; set; }
}

public sealed class CurrentDto
{
    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("feelslike_c")]
    public decimal FeelsLikeC { get; set; }

    [JsonPropertyName("condition")]
    public ConditionDto Condition { get; set; } = new();

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("wind_kph")]
    public decimal WindKph { get; set; }

    [JsonPropertyName("pressure_mb")]
    public decimal PressureMb { get; set; }

    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; } = string.Empty;
}
