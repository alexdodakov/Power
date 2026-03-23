using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Web.Controllers;

public sealed class WeatherController : Controller
{
    private readonly IWeatherFacade _facade;
    private readonly WeatherApiOptions _options;

    public WeatherController(IWeatherFacade facade, IOptions<WeatherApiOptions> options)
    {
        _facade = facade;
        _options = options.Value;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("report")]
    public async Task<IActionResult> Report(CancellationToken cancellationToken)
    {
        var query = new GetWeatherReportQuery
        {
            Latitude = _options.Latitude,
            Longitude = _options.Longitude,
            Days = 3,
        };

        var report = await _facade.GetWeatherReportAsync(query, cancellationToken);
        return Ok(report);
    }
}
