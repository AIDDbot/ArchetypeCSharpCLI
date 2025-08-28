# Backlog for Archetype CSharp CLI

> Epic Priority Legend: ❗️ Critical | ❗ High  |❕ Normal

> Feature Status Legend: ⛔ BLOCKED | ⏳ PENDING | ⛪ SPECIFIED | ✏️ DESIGNED | 📝 PLANNED | ✨ CODED | ✅ TESTED | ⛲ CLEANED | ✔️ RELEASED 

## E1 CLI Core ❗️ Critical

- Minimal, reliable CLI host that everything else builds on.
  
### F1.1 CLI host skeleton and help/version ✔️ RELEASED

- Dependencies:
  
- Project Requirements:
  - R1 CLI Host and Basic Commands

Provide a console entry point with `--help` and `--version`, wiring a command framework (System.CommandLine).

- Links:
  - [Feature Specification](./backlog/feat-cli-host.spec.md)
  - [Design Document](./backlog/feat-cli-host.design.md)
  - [Implementation Plan](./backlog/feat-cli-host.plan.md)
  - Test status: ✅ Passed (xUnit) — process-level checks for help/version
  - Structure: [STRUCTURE.md](../STRUCTURE.md)

### F1.2 Command routing and option validation ✔️ RELEASED

- Dependencies:
  - F1.1 CLI host skeleton and help/version
- Project Requirements:
  - R1 CLI Host and Basic Commands

Define subcommands, options, and basic validation; map to handlers and stable exit codes.

- Links:
  - [Feature Specification](./backlog/feat-command-routing.spec.md)
  - [Design Document](./backlog/feat-command-routing.design.md)
  - [Implementation Plan](./backlog/feat-command-routing.plan.md)
  - Test status: ✅ Passed (xUnit) — routing, help, required and custom validation ([report](./backlog/feat-command-routing.test.md))
  - Structure: [STRUCTURE.md](../STRUCTURE.md)

## E3 Configuration & Logging ❗ High

- Cross-cutting behavior needed early for predictability and diagnostics.

### F3.1 Configuration pipeline (appsettings + env) ✔️ RELEASED

- Dependencies:
  
- Project Requirements:
  - R3 Configuration and Logging

Load appsettings.json, appsettings.{Environment}.json, and environment variables; expose typed access.

- Links:
  - [Feature Specification](./backlog/feat-config-pipeline.spec.md)
  - [Design Document](./backlog/feat-config-pipeline.design.md)
  - [Implementation Plan](./backlog/feat-config-pipeline.plan.md)
  - Test status: ✅ Passed (xUnit) — precedence, defaults, and env overrides ([report](./backlog/feat-config-pipeline.test.md))
  - Structure: [STRUCTURE.md](../STRUCTURE.md)

### F3.2 Structured console logging ✅ TESTED

- Dependencies:
  
- Project Requirements:
  - R3 Configuration and Logging

Add Microsoft.Extensions.Logging console provider with levels and scopes; ensure helpful defaults.

- Links:
  - [Feature Specification](./feats/feat-logging-console.spec.md)
  - [Design Document](./feats/feat-logging-console.design.md)
  - [Implementation Plan](./feats/feat-logging-console.plan.md)
  - Test status: ✅ Passed (xUnit) — console output levels, scopes, and defaults

### F3.3 Bind typed options/POCOs ⏳ PENDING

- Dependencies:
  - F3.1 Configuration pipeline (appsettings + env)
- Project Requirements:
  - R3 Configuration and Logging

Provide typed configuration binding for command options and services defaults.

- Links:
  - [Feature Specification](./feats/feat-config-binding.spec.md)
  - [Design Document](./feats/feat-config-binding.design.md)
  - [Implementation Plan](./feats/feat-config-binding.plan.md)

## E4 HTTP & Resilience ❗ High

- Foundations for safe external API calls used by commands.

### F4.1 Typed HttpClient with sensible timeouts ⏳ PENDING

- Dependencies:
  
- Project Requirements:
  - R4 Dependency Injection and HTTP Resilience
  - R6 HTTP and Resilience (error handling)

Provide typed HttpClient registration, default timeout, and base handlers.

- Links:
  - [Feature Specification](./feats/feat-http-typed-client.spec.md)
  - [Design Document](./feats/feat-http-typed-client.design.md)
  - [Implementation Plan](./feats/feat-http-typed-client.plan.md)

### F4.2 Error handling and non-2xx mapping ⛔ BLOCKED

- Dependencies:
  - F4.1 Typed HttpClient with sensible timeouts
- Project Requirements:
  - R6 HTTP and Resilience
  - R7 Errors and Exit Codes

Handle timeouts, network errors, and non-2xx responses with clear messages and mapped exit codes.

- Links:
  - [Feature Specification](./feats/feat-http-error-handling.spec.md)
  - [Design Document](./feats/feat-http-error-handling.design.md)
  - [Implementation Plan](./feats/feat-http-error-handling.plan.md)

### F4.3 DTOs and mapping for external APIs ⛔ BLOCKED

- Dependencies:
  - F4.1 Typed HttpClient with sensible timeouts
- Project Requirements:
  - R2 Weather Command

Define minimal DTOs for ip-api.com and open-meteo.com and map to internal models.

- Links:
  - [Feature Specification](./feats/feat-dto-mapping.spec.md)
  - [Design Document](./feats/feat-dto-mapping.design.md)
  - [Implementation Plan](./feats/feat-dto-mapping.plan.md)

## E2 Weather Experience ❗ High

- Demonstrates a real-world command using external data.

### F2.1 IP Geolocation client ⛔ BLOCKED

- Dependencies:
  - F4.1 Typed HttpClient with sensible timeouts
  - F4.3 DTOs and mapping for external APIs
- Project Requirements:
  - R2 Weather Command

Call ip-api.com to resolve latitude/longitude for the current IP.

- Links:
  - [Feature Specification](./feats/feat-geo-ip-client.spec.md)
  - [Design Document](./feats/feat-geo-ip-client.design.md)
  - [Implementation Plan](./feats/feat-geo-ip-client.plan.md)

### F2.2 Weather API client ⛔ BLOCKED

- Dependencies:
  - F4.1 Typed HttpClient with sensible timeouts
  - F4.3 DTOs and mapping for external APIs
- Project Requirements:
  - R2 Weather Command

Call Open‑Meteo to retrieve the current weather based on coordinates.

- Links:
  - [Feature Specification](./feats/feat-weather-client.spec.md)
  - [Design Document](./feats/feat-weather-client.design.md)
  - [Implementation Plan](./feats/feat-weather-client.plan.md)

### F2.3 Weather command handler ⛔ BLOCKED

- Dependencies:
  - F1.2 Command routing and option validation
  - F2.1 IP Geolocation client
  - F2.2 Weather API client
  - F3.1 Configuration pipeline (appsettings + env)
  - F3.2 Structured console logging
- Project Requirements:
  - R2 Weather Command
  - R7 Errors and Exit Codes

Implement the `weather` command to compose geolocation + weather, handle `--timeout`, and map errors to exit codes.

- Links:
  - [Feature Specification](./feats/feat-weather-command.spec.md)
  - [Design Document](./feats/feat-weather-command.design.md)
  - [Implementation Plan](./feats/feat-weather-command.plan.md)

### F2.4 Output formatting and units option ⛔ BLOCKED

- Dependencies:
  - F2.3 Weather command handler
- Project Requirements:
  - R2 Weather Command

Provide human-friendly output and `--units` (metric|imperial) conversion.

- Links:
  - [Feature Specification](./feats/feat-weather-output.spec.md)
  - [Design Document](./feats/feat-weather-output.design.md)
  - [Implementation Plan](./feats/feat-weather-output.plan.md)

### F2.5 Raw JSON output option ⛔ BLOCKED

- Dependencies:
  - F2.3 Weather command handler
- Project Requirements:
  - R2 Weather Command

Add `--raw` flag to print raw JSON from providers.

- Links:
  - [Feature Specification](./feats/feat-weather-raw.spec.md)
  - [Design Document](./feats/feat-weather-raw.design.md)
  - [Implementation Plan](./feats/feat-weather-raw.plan.md)

## E5 Exit Codes & Error Experience ❕ Normal

- Make failures predictable and actionable.

### F5.1 Exit code policy and mapping ⏳ PENDING

- Dependencies:
  
- Project Requirements:
  - R7 Errors and Exit Codes

Define exit code ranges for validation, network errors, and unexpected exceptions.

- Links:
  - [Feature Specification](./feats/feat-exit-codes.spec.md)
  - [Design Document](./feats/feat-exit-codes.design.md)
  - [Implementation Plan](./feats/feat-exit-codes.plan.md)

### F5.2 User-friendly error messages ⛔ BLOCKED

- Dependencies:
  - F5.1 Exit code policy and mapping
- Project Requirements:
  - R7 Errors and Exit Codes

Provide concise, actionable messages (hide stack traces in Release by default).

- Links:
  - [Feature Specification](./feats/feat-error-messages.spec.md)
  - [Design Document](./feats/feat-error-messages.design.md)
  - [Implementation Plan](./feats/feat-error-messages.plan.md)

## Additional Information

- Git repository: https://github.com/AIDDbot/ArchetypeCSharpCLI
- PRD Document: ./PRD.md
- DOMAIN Models: ./DOMAIN.md
- SYSTEMS Architecture: ./SYSTEMS.md

> End of BACKLOG for Archetype CSharp CLI, last updated 2025-08-28.
