using System.Text.Json.Serialization;

namespace WeatherApp.Infrastructure.Dtos;

public sealed class CurrentWeatherResponseDto
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
    public string LastUpdatedFormatted { get; set; } = string.Empty;
}
