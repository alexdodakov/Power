using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Dtos;

namespace WeatherApp.Infrastructure.Mapping;

public interface IWeatherApiResponseMapper
{
    WeatherApiPayload ToPayload(ForecastWeatherResponseDto response);
}
