using FluentValidation;
using WeatherApp.Api.Mapping;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Mapping;
using WeatherApp.Application.Models;
using WeatherApp.Application.Services;
using WeatherApp.Application.Validation;
using WeatherApp.Infrastructure.Clients;
using WeatherApp.Infrastructure.Configuration;
using WeatherApp.Infrastructure.Http;
using WeatherApp.Infrastructure.Mapping;
using WeatherApp.Infrastructure.Parsing;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("WeatherApi"));
builder.Services.AddSingleton<IValidator<GetWeatherReportQuery>, GetWeatherReportQueryValidator>();
builder.Services.AddScoped<IWeatherFacade, WeatherFacade>();
builder.Services.AddScoped<IWeatherConditionTextTranslator, WeatherConditionTextTranslator>();
builder.Services.AddTransient<IDateTimeParser, DateTimeParser>();
builder.Services.AddTransient<IDateParser, DateParser>();
builder.Services.AddScoped<IWeatherReportMapper, WeatherReportMapper>();
builder.Services.AddHttpClient<WeatherApiHttpClient>();
builder.Services.AddScoped<IWeatherApiClient, WeatherApiClient>();
builder.Services.AddScoped<IWeatherApiResponseMapper, WeatherApiResponseMapper>();

builder.Services.AddSingleton<IWeatherReportDtoMapper, WeatherReportMapping>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();




