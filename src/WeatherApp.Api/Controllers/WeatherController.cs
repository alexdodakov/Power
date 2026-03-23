using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Models;
using WeatherApp.Api.Mapping;
using WeatherApp.Api.Models;

namespace WeatherApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class WeatherController : ControllerBase
{
    private readonly IWeatherFacade _facade;
    private readonly IWeatherReportDtoMapper _mapper;

    public WeatherController(IWeatherFacade facade, IWeatherReportDtoMapper mapper)
    {
        _facade = facade;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<WeatherPageDto>> Get([FromQuery] GetWeatherReportQuery query, CancellationToken cancellationToken)
    {
        var report = await _facade.GetWeatherReportAsync(query, cancellationToken);
        return Ok(_mapper.Map(report));
    }
}
