namespace WeatherApp.Application.Models;

public sealed class GetWeatherReportQuery
{
    public decimal Latitude { get; init; }

    public decimal Longitude { get; init; }

    public int Days { get; init; } = 3;
}
