using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ArchetypeCSharpCLI;

public static class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandApp<RootCommand>();
        app.Configure(cfg =>
        {
            cfg.SetApplicationName("archetype");
        });

        var argList = args ?? Array.Empty<string>();
        if (argList.Length == 0)
        {
            return app.Run(new[] { "--help" });
        }

    return app.Run(argList);
    }

    private static string GetInformationalVersion()
    {
        var asm = Assembly.GetExecutingAssembly();
        var info = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        return string.IsNullOrWhiteSpace(info)
            ? asm.GetName().Version?.ToString() ?? "0.0.0"
            : info;
    }

    // Default/root command using Spectre.Console.Cli
    private sealed class RootCommand : Command<RootSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] RootSettings settings)
        {
            if (settings.Version)
            {
                AnsiConsole.MarkupLine($"[green]{GetInformationalVersion()}[/]");
                return 0;
            }

            // Nothing else to do yet; suggest help
            AnsiConsole.MarkupLine("[grey]Run with --help for usage.[/]");
            return 0;
        }
    }

    private sealed class RootSettings : CommandSettings
    {
        [CommandOption("-v|--version")]
        [Description("Show version information and exit")]
        public bool Version { get; init; }
    }
}
