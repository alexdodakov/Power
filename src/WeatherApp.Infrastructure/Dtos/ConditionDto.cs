using System.Text.Json.Serialization;

namespace WeatherApp.Infrastructure.Dtos;

public sealed class ConditionDto
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;
}
