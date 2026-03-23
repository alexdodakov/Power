namespace WeatherApp.Infrastructure.Configuration;

public sealed class WeatherApiOptions
{
    public string BaseUrl { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
