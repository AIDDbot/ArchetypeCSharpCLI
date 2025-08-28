using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Reflection;

namespace ArchetypeCSharpCLI;

public static class Program
{
  /// <summary>
  /// Entry point for the Archetype C# CLI. Builds the command-line parser and
  /// delegates execution. Defaults to showing help when no arguments are provided.
  /// </summary>
  /// <param name="args">Command-line arguments passed to the application.</param>
  /// <returns>Process exit code (0 for success; non-zero on failures).</returns>
  public static int Main(string[] args)
  {
    var parser = BuildParser();
    // Show help by default when no args are provided
    var argList = args is { Length: > 0 } ? args : new[] { "--help" };
    return parser.InvokeAsync(argList).GetAwaiter().GetResult();
  }

  /// <summary>
  /// Gets the semantic version for this application, preferring the
  /// <see cref="AssemblyInformationalVersionAttribute"/> value when present.
  /// </summary>
  /// <returns>The application version string.</returns>
  private static string GetInformationalVersion()
  {
    var asm = Assembly.GetExecutingAssembly();
    var info = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    return string.IsNullOrWhiteSpace(info)
        ? asm.GetName().Version?.ToString() ?? "0.0.0"
        : info;
  }

  /// <summary>
  /// Builds the System.CommandLine parser and wires common behaviors:
  /// default help, <c>--version</c> (<c>-v</c>) printing, and help precedence.
  /// </summary>
  /// <returns>A configured <see cref="Parser"/> ready to invoke.</returns>
  private static Parser BuildParser()
  {
    var root = new RootCommand("Archetype C# CLI — minimal host with help and version");

    // Define version options (long and short)
    var versionLong = new Option<bool>("--version", "Show version information and exit");
    var versionShort = new Option<bool>("-v", "Show version information and exit");
    root.AddOption(versionLong);
    root.AddOption(versionShort);

    var builder = new CommandLineBuilder(root)
      .UseHelp()
      .UseParseErrorReporting()
      .CancelOnProcessTermination()
      .AddMiddleware(async (context, next) =>
      {
        var argsContainHelp = context.ParseResult.Tokens.Any(t => t.Value is "--help" or "-h" or "-?");
        var wantsVersion = context.ParseResult.GetValueForOption(versionLong) || context.ParseResult.GetValueForOption(versionShort);

        if (wantsVersion && !argsContainHelp)
        {
          Console.WriteLine(GetInformationalVersion());
          context.ExitCode = 0;
          return;
        }

        // No subcommands yet: if nothing requested, let help middleware run
        await next(context);
      });

    return builder.Build();
  }
}
