# Project Structure

Follows Screaming Architecture and Separation of Concerns principles.

```
/ (repo root)
├─ src/
│  ├─ ArchetypeCSharpCLI/            # CLI application (presentation layer)
│  │  ├─ ArchetypeCSharpCLI.csproj
│  │  ├─ Program.cs
│  │  ├─ Configuration/              # F3.1 configuration pipeline
│  │  │  ├─ AppConfig.cs            # Typed config POCO with defaults
│  │  │  ├─ AppSettings.cs          # Cached accessor to current AppConfig
│  │  │  └─ ConfigBuilder.cs        # Builds IConfiguration/AppConfig from files+env
│  │  └─ Commands/
│  │     ├─ CommandFactory.cs       # Builds root parser and subcommands
│  │     └─ Hello/
│  │        ├─ HelloOptions.cs      # Input model for 'hello' command
│  │        └─ HelloCommandHandler.cs # Handler for 'hello' behavior
│  └─ ArchetypeCSharpCLI.Tests/      # Test project
│     ├─ ArchetypeCSharpCLI.Tests.csproj
│     ├─ CliHostTests.cs             # Host-level help/version tests
│     ├─ CommandRoutingTests.cs      # F1.2 routing & validation tests
│     └─ ConfigPipelineTests.cs      # F3.1 config pipeline precedence & defaults tests
├─ ArchetypeCSharpCLI.sln
├─ global.json
├─ README.md
└─ LICENSE
```

Notes
- CLI framework: System.CommandLine (beta4). Root command exposes --help (default) and --version/-v.
- More features will be grouped by domain inside `src/` as they grow (e.g., commands, services, adapters).

Configuration
- Config sources: appsettings.json -> appsettings.{Environment}.json -> Environment Variables
- Environment is taken from DOTNET_ENVIRONMENT or ASPNETCORE_ENVIRONMENT, default Production.
- Env vars can use flat keys (HttpTimeoutSeconds) or nested via App__HttpTimeoutSeconds; nested overrides flat.

## Bill of materials

- Runtime & SDK
	- .NET SDK: 9.0.304 (pinned via `global.json`)
	- Target Framework: net9.0

- Application Libraries
	- System.CommandLine: 2.0.0-beta4.22272.1
	- C# Language: Nullable enabled, implicit usings enabled

- Testing Libraries
	- xUnit: 2.9.2
	- xunit.runner.visualstudio: 2.8.2
	- Microsoft.NET.Test.Sdk: 17.11.1
	- FluentAssertions: 6.12.1

- Tooling
	- .NET CLI (`dotnet build`, `dotnet test`, `dotnet run`)
	- dotnet format (whitespace)
