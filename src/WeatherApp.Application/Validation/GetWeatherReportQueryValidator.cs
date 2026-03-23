using FluentValidation;
using WeatherApp.Application.Models;

namespace WeatherApp.Application.Validation;

public sealed class GetWeatherReportQueryValidator : AbstractValidator<GetWeatherReportQuery>
{
    public GetWeatherReportQueryValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90m, 90m);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180m, 180m);

        RuleFor(x => x.Days)
            .InclusiveBetween(1, 10);
    }
}
