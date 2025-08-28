# F3.2 Structured console logging Design 

## Overview

Introduce a single LoggerFactory configured from AppSettings to write single-line console logs with timestamps and scopes. Provide helper `Log` static to access category loggers across the app without introducing a DI container.

## Data Models

- None required; reuses existing AppConfig.LogLevel string.

## Components

### Log (static helper)

- Purpose: Provide access to an `ILogger`/`ILogger<T>` and own a shared `LoggerFactory`.
- Interfaces:
  - `ILogger<T> For<T>()`
  - `ILogger For(Type type)`
  - `ILogger For(string category)`
- Dependencies: Microsoft.Extensions.Logging, AppSettings for level, Console provider.
- Reuses: AppSettings for configuration.

## User interface

Command-line interaction unchanged. Logs appear on stderr/stdout depending on provider (Console logger writes to stdout by default). We keep messages concise.

### Root command middleware

- Purpose: Create a scope with command and raw args for correlation.
- Command: root / any subcommand.

## Aspects

### Monitoring

- Single-line logs with HH:mm:ss, level, category, and scope values. Scopes include `cmd` and `args`.

### Security

- Avoid logging secrets in scope/args.

### Error Handling

- Logging robust on invalid level; defaults to Information.

## Architecture

Simple static factory; no DI container introduced to keep CLI minimal.

### File Structure

- src/ArchetypeCSharpCLI/Logging/Log.cs — factory and helpers
- References added to Microsoft.Extensions.Logging.Console

> End of Feature Design for F3.2, last updated 2025-08-28.
