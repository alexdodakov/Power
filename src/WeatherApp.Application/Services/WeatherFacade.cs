using FluentValidation;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Mapping;
using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Application.Services;

public sealed class WeatherFacade : IWeatherFacade
{
    private readonly IWeatherApiClient _weatherApiClient;
    private readonly IValidator<GetWeatherReportQuery> _validator;
    private readonly IWeatherReportMapper _mapper;

    public WeatherFacade(
        IWeatherApiClient weatherApiClient,
        IValidator<GetWeatherReportQuery> validator,
        IWeatherReportMapper mapper)
    {
        _weatherApiClient = weatherApiClient;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<WeatherReport> GetWeatherReportAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(query, cancellationToken);

        var payload = await _weatherApiClient.GetWeatherAsync(query, cancellationToken);

        return _mapper.Map(payload);
    }
}
