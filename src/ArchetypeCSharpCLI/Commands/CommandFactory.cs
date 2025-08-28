using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace ArchetypeCSharpCLI.Commands;

public static class CommandFactory
{
  public static Parser BuildParser(Func<string> versionProvider)
  {
    var root = new RootCommand("Archetype C# CLI — minimal host with help, version, and routing");

    // version options
    var versionLong = new Option<bool>("--version", "Show version information and exit");
    var versionShort = new Option<bool>("-v", "Show version information and exit");
    root.AddOption(versionLong);
    root.AddOption(versionShort);

    // subcommands
    root.AddCommand(BuildHelloCommand());

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
          Console.WriteLine(versionProvider());
          context.ExitCode = 0;
          return;
        }

        await next(context);
      });

    return builder.Build();
  }

  private static Command BuildHelloCommand()
  {
    var cmd = new Command("hello", "Print a greeting for the provided name.");

    var nameOption = new Option<string>(new[] {"--name", "-n"}, "Name to greet")
    {
      IsRequired = true
    };

    // validation: non-empty/whitespace
    nameOption.AddValidator(result =>
    {
      var value = result.GetValueOrDefault<string>();
      if (string.IsNullOrWhiteSpace(value))
        result.ErrorMessage = "--name must be a non-empty value";
    });

    cmd.AddOption(nameOption);

    cmd.SetHandler((string name) =>
    {
      Console.WriteLine($"Hello, {name}!");
    }, nameOption);

    return cmd;
  }
}
