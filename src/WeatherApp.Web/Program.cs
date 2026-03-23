using FluentValidation;
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
builder.Services.AddScoped<IWeatherReportMapper, WeatherReportMapper>();
builder.Services.AddTransient<IDateTimeParser, DateTimeParser>();
builder.Services.AddTransient<IDateParser, DateParser>();
builder.Services.AddScoped<IWeatherApiClient, WeatherApiClient>();
builder.Services.AddScoped<IWeatherApiResponseMapper, WeatherApiResponseMapper>();
builder.Services.AddHttpClient<WeatherApiHttpClient>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=Index}/{id?}");

app.Run();




