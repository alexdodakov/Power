using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Application.Abstractions;

public interface IWeatherFacade
{
    Task<WeatherReport> GetWeatherReportAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default);
}
