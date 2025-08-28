# Systems Architecture for Archetype CSharp CLI

Follows the glossary of terms and concepts from AIDDbot Glossary.

## Overview

Archetype CSharp CLI follows a layered CLI architecture, designed for simplicity, robustness, and developer experience with .NET platform conventions and Microsoft.Extensions.* libraries.

## Presentation Tier

### A1 CLI Application (Console)

**Purpose:** User entry point that parses commands/options, renders help, and orchestrates command execution.

**Technology Stack:**

- Language: c-sharp
- Framework: .net (Console)
- Key Libraries: System.CommandLine or Spectre.Console.Cli, Microsoft.Extensions.Logging
- Other Packages: Microsoft.Extensions.Configuration, System.Text.Json

**Responsibilities:**

- Parse args and validate options
- Display help, usage, and version
- Map commands to handlers and exit codes

## Application Tier

### A2 Command/Service Layer

**Purpose:** Encapsulates command handlers and reusable services (geolocation, weather) wired via DI and using typed HttpClient.

**Technology Stack:**

- Language: c-sharp
- Framework: asp.net (hosting abstractions via Microsoft.Extensions)
- Key Libraries: Microsoft.Extensions.DependencyInjection, System.Net.Http
- Other Packages: Polly (optional), Serilog (optional)

**Responsibilities:**

- Implement command handlers (e.g., `weather`)
- Coordinate calls to external APIs with resilience
- Enforce business rules and map errors to exit codes

## Data Tier

### D1 Local Configuration Store

**Database Type:** n/a (file-based config)
**Technology:** appsettings.json + environment variables

**Responsibilities:**

- Provide defaults and environment overrides
- Supply HTTP timeouts, log levels, and feature flags

## Integration Patterns

### I1 Outbound REST Calls

**Type:** REST API
**Purpose:** Retrieve IP-based geolocation and weather data
**Protocol:** HTTPS
**Data Format:** JSON

Providers:
- IP Geolocation API (ip-api.com)
- Open‑Meteo API (open-meteo.com)

## Security Architecture

### Authentication & Authorization

**Authentication Method:** None for local CLI usage; outbound calls are anonymous HTTPS.
**Session Management:** Not applicable.
**Authorization Pattern:** Not applicable.

Hardening:
- All external traffic over HTTPS
- No secrets stored; configuration via env vars and files
- Minimal PII; do not persist IP-derived data

## Systems Architecture Diagram

```mermaid
C4Container
  title Archetype CSharp CLI — Containers
  Person(dev, "Developer")
  Container(cli, "CLI Application", "C#/.NET Console", "Parses commands, logs, config")
  Container_Boundary(core, "Command/Service Layer") {
    Container(handlers, "Command Handlers", "C#", "Implements commands")
    Container(services, "Services", "C#", "Geo + Weather services, DI")
  }
  System_Ext(ipApi, "IP Geolocation API", "HTTPS/JSON")
  System_Ext(openMeteo, "Open‑Meteo API", "HTTPS/JSON")

  Rel(dev, cli, "Runs commands")
  Rel(cli, handlers, "Dispatch")
  Rel(handlers, services, "Calls")
  Rel(services, ipApi, "GET /json", "HTTPS")
  Rel(services, openMeteo, "GET /forecast", "HTTPS")
```

## Additional Information

- Git repository: https://github.com/AIDDbot/ArchetypeCSharpCLI
- PRD Document: ./PRD.md
- DOMAIN Models: ./DOMAIN.md
- BACKLOG of features: ./BACKLOG.md

> End of SYSTEMS for Archetype CSharp CLI, last updated on 2025-08-28.
