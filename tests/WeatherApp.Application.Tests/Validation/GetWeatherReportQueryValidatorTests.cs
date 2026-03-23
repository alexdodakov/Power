using FluentValidation.TestHelper;
using WeatherApp.Application.Models;
using WeatherApp.Application.Validation;
using Xunit;

namespace WeatherApp.Application.Tests;

public sealed class GetWeatherReportQueryValidatorTests
{
    private readonly GetWeatherReportQueryValidator _validator = new();

    [Fact]
    public void Validate_WhenQueryIsValid_ShouldNotContainErrors()
    {
        var query = new GetWeatherReportQuery
        {
            Latitude = 55.7558m,
            Longitude = 37.6176m,
            Days = 3,
        };

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenLatitudeLongitudeAndDaysAreInvalid_ShouldContainErrors()
    {
        var query = new GetWeatherReportQuery
        {
            Latitude = 120m,
            Longitude = 200m,
            Days = 0,
        };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Latitude);
        result.ShouldHaveValidationErrorFor(x => x.Longitude);
        result.ShouldHaveValidationErrorFor(x => x.Days);
    }
}
