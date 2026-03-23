using WeatherApp.Api.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Api.Mapping;

public interface IWeatherReportDtoMapper
{
    WeatherPageDto Map(WeatherReport report);
}
