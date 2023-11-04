using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneValidator.App.Extensions;
using PhoneValidator.App.Models;
using PhoneValidator.App.Services.Implementations;
using PhoneValidator.App.Services.Interfaces;
using PhoneValidator.Application.Configurations;
using PhoneValidator.Application.DTOs;

var builder = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", false, true);

var config = builder.Build();

var dbString = config.GetConnectionString("Sqlite");

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddOptions<ApiConfig>().Bind(config.GetSection("ApiConfig"));
        services.AddProjectsServices(dbString);
        services.AddScoped<IJobService, JobService>();
    })
    .Build();

try
{
    await Proceed(host.Services);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

await host.RunAsync();
return;

static async Task Proceed(IServiceProvider services)
{
    var filePath = @"C:\GitHub\PhoneNumberValidation\file\testPhones.csv";
    var records = LoadDataFromCsv(filePath);
    // var appLifetime = services.GetRequiredService<IHostApplicationLifetime>();

    var jobService = services.GetService<IJobService>();
  
    records = await jobService!.Proceed(records);

    UpdateCsv(filePath, records);

    // appLifetime.StopApplication();
}

static List<PhoneNumberDto> LoadDataFromCsv(string filePath)
{
    using var sr = new StreamReader(filePath);
    using var csv = new CsvReader(sr, CultureInfo.InvariantCulture);

    var records = csv.GetRecords<PhoneNumberCsv>().ToList();

    var dto = records.Select(x => new PhoneNumberDto
    {
        DefaultCountryCode = x.DefaultCountryCode,
        PhoneType = x.PhoneType,
        Number = x.Number
    }).ToList();

    return dto;
}

static void UpdateCsv(string filePath, List<PhoneNumberDto> dtos)
{
    using (var writer = new StreamWriter(filePath))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.WriteRecords(dtos);
    }
}