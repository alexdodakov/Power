using WeatherApp.Application.Models;

namespace WeatherApp.Application.Abstractions;

public interface IWeatherApiClient
{
    Task<WeatherApiPayload> GetWeatherAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default);
}
