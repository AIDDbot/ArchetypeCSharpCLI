using System;
using System.Threading;
using System.Threading.Tasks;
using ArchetypeCSharpCLI.Http.GeoIp;
using ArchetypeCSharpCLI.Http.Weather;
using ArchetypeCSharpCLI.Domain;

namespace ArchetypeCSharpCLI.Commands.Weather;

public static class WeatherHandler
{
  public static async Task<int> HandleAsync(decimal? lat, decimal? lon, int? timeoutSeconds, string units = "metric", bool raw = false)
  {
    // Resolve services from a simple service locator available in OptionsBootstrap
    var provider = OptionsBootstrap.Services ?? throw new InvalidOperationException("DI services not initialized");

    var geo = provider.GetService(typeof(IGeoIpClient)) as IGeoIpClient;
    var weather = provider.GetService(typeof(IWeatherClient)) as IWeatherClient;

    if (weather == null)
    {
      Console.Error.WriteLine("Weather client is not registered.");
      return ExitCodes.Unexpected;
    }

    decimal latitude, longitude;

    if (lat.HasValue && lon.HasValue)
    {
      latitude = lat.Value;
      longitude = lon.Value;
    }
    else
    {
      if (geo == null)
      {
        Console.Error.WriteLine("GeoIP client not available to resolve coordinates.");
        return ExitCodes.Unexpected;
      }

      var geoResult = await geo.GetLocationAsync();
      if (!geoResult.IsSuccess)
      {
        Console.Error.WriteLine(geoResult.ErrorMessage);
        return ExitCodes.NetworkOrProviderError;
      }

      var loc = geoResult.Location!;
      latitude = loc.Latitude;
      longitude = loc.Longitude;
    }

    using var cts = timeoutSeconds.HasValue ? new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds.Value)) : new CancellationTokenSource();

    var weatherResult = await weather.GetCurrentAsync(latitude, longitude, units, raw, cts.Token);
    if (!weatherResult.IsSuccess)
    {
      Console.Error.WriteLine(weatherResult.ErrorMessage);
      return ExitCodes.NetworkOrProviderError;
    }
    if (raw && !string.IsNullOrEmpty(weatherResult.RawJson))
    {
      Console.WriteLine(weatherResult.RawJson);
      return ExitCodes.Success;
    }

    var report = weatherResult.Report!;
    // Write human-friendly output
    Console.WriteLine($"{report.Condition} — {report.Temperature} {report.Units} (observed at {report.ObservedAt:u})");
    return ExitCodes.Success;
  }
}
