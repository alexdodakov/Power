using Microsoft.AspNetCore.Mvc;
using WeatherApp.Api.Controllers;
using WeatherApp.Api.Mapping;
using WeatherApp.Api.Models;
using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;
using Xunit;

namespace WeatherApp.Api.Tests;

public sealed class WeatherControllerTests
{
    [Fact]
    public async Task Get_ShouldReturnOkWithMappedDto()
    {
        var report = new WeatherReport { CityName = "Moscow" };
        var dto = new WeatherPageDto("Moscow", new CurrentWeatherDto(1, 1, "Cloudy", "//icon", 10, 2, 760, DateTime.UtcNow), [], []);
        var facade = new FakeWeatherFacade { Result = report };
        var mapper = new FakeWeatherReportDtoMapper { Result = dto };
        var controller = new WeatherController(facade, mapper);
        var query = new GetWeatherReportQuery { Latitude = 55.7558m, Longitude = 37.6176m, Days = 3 };
        using var cts = new CancellationTokenSource();

        var actionResult = await controller.Get(query, cts.Token);

        var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
        var value = Assert.IsType<WeatherPageDto>(ok.Value);

        Assert.Same(report, facade.ReceivedReportRequestResultSource);
        Assert.Equal(query, facade.ReceivedQuery);
        Assert.Equal(cts.Token, facade.ReceivedCancellationToken);
        Assert.Same(report, mapper.ReceivedReport);
        Assert.Same(dto, value);
    }
}
