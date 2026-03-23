using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Application.Mapping;

public interface IWeatherReportMapper
{
    WeatherReport Map(WeatherApiPayload payload);
}
