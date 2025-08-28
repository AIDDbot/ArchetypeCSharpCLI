using System.CommandLine;
using ArchetypeCSharpCLI.Http.GeoIp;
using ArchetypeCSharpCLI.Http.Weather;

namespace ArchetypeCSharpCLI.Commands.Weather;

public static class WeatherCommand
{
  public static Command Build()
  {
    var cmd = new Command("weather", "Show current weather for the current IP or provided coordinates");

    var lat = new Option<decimal?>("--lat", "Latitude in decimal degrees");
    var lon = new Option<decimal?>("--lon", "Longitude in decimal degrees");
    var timeout = new Option<int?>("--timeout", () => null, "Timeout in seconds for the operation");
    var units = new Option<string>(new[] { "--units" }, () => "metric", "Units to display: metric or imperial");
    var raw = new Option<bool>("--raw", "Print raw JSON from provider");

    cmd.AddOption(lat);
    cmd.AddOption(lon);
    cmd.AddOption(timeout);

    cmd.SetHandler(async (decimal? latVal, decimal? lonVal, int? timeoutVal, string unitsVal, bool rawVal) =>
    {
      var opts = new { Latitude = latVal, Longitude = lonVal, TimeoutSeconds = timeoutVal, Units = unitsVal };
      var code = await WeatherHandler.HandleAsync(opts.Latitude, opts.Longitude, opts.TimeoutSeconds, opts.Units, rawVal);
      if (code != 0)
      {
        Console.Error.WriteLine($"weather command failed with exit code {code}");
        Environment.ExitCode = code;
      }
    }, lat, lon, timeout, units, raw);

    return cmd;
  }
}
