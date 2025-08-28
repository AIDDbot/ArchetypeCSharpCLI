# Archetype CSharp CLI

Archetype CSharp CLI is a starter template for building .NET command‑line applications with a clean structure, configuration, logging, DI, and a sample weather command. It’s designed to help developers clone, run, and extend quickly.


## Docs

- [docs/PRD.md](./docs/PRD.md)
- [docs/DOMAIN.md](./docs/DOMAIN.md)
- [docs/SYSTEMS.md](./docs/SYSTEMS.md)
- [docs/BACKLOG.md](./docs/BACKLOG.md)
- [docs/STRUCTURE.md](./docs/STRUCTURE.md)
- [docs/MANUAL.md](./docs/MANUAL.md)


## Usage

- Help
  - `dotnet run --project src/ArchetypeCSharpCLI -- --help`
- Version
  - `dotnet run --project src/ArchetypeCSharpCLI -- --version`
  - `dotnet run --project src/ArchetypeCSharpCLI -- -v`

## HTTP Defaults

- The CLI registers `IHttpClientFactory` at startup and applies sane defaults via `AddHttpCore`.
- Default timeout is `App:HttpTimeoutSeconds` (or `HttpTimeoutSeconds`) clamped to [1, 60]. Default is 30 seconds.
- All `HttpClient` instances include `User-Agent: ArchetypeCSharpCLI/{InformationalVersion}`.
- Override via configuration files or environment variables, e.g.:
  - env var: `App__HttpTimeoutSeconds=10`
  - `appsettings.Development.json` takes precedence over `appsettings.json`; environment variables override files.


## Author & Links

- Author: [Alberto Basalo](https://albertobasalo.dev)
- Socials:
  - [LinkedIn](https://www.linkedin.com/in/albertobasalo/)
  - [GitHub](https://github.com/albertobasalo)
- AIDDbot.com Blog: [AIDDbot.com](https://aiddbot.com)
- AIDDbot org at GitHub: [GitHub/AIDDbot](https://github.com/AIDDbot)
- This Repository: [GitHub/AIDDbot/ArchetypeCSharpCLI](https://github.com/AIDDbot/ArchetypeCSharpCLI)
- Academia en Español: [AI code Academy](https://aicode.academy)
- Curso: [Aprende a usar GitHub Copilot profesionalmente](https://aicode.academy/cursos/vs-code-copilot/)

