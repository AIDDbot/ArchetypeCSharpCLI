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

        cmd.AddOption(lat);
        cmd.AddOption(lon);
        cmd.AddOption(timeout);

        cmd.SetHandler(async (decimal? latVal, decimal? lonVal, int? timeoutVal) =>
        {
            var opts = new { Latitude = latVal, Longitude = lonVal, TimeoutSeconds = timeoutVal };
            var code = await WeatherHandler.HandleAsync(opts.Latitude, opts.Longitude, opts.TimeoutSeconds);
            if (code != 0)
            {
                Console.Error.WriteLine($"weather command failed with exit code {code}");
                Environment.ExitCode = code;
            }
        }, lat, lon, timeout);

        return cmd;
    }
}
