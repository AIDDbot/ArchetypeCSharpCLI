# F3.1 Configuration pipeline (appsettings + env) Specification

Provide a simple, predictable configuration pipeline for the CLI: load appsettings.json, appsettings.{Environment}.json, and environment variables; expose typed access for defaults like HTTP timeout and log level.

- Epic: E3 Configuration & Logging
- Priority: ❗ High
- Product Requirements:
  - R3 Configuration and Logging (from PRD)

## User Story 1

- As a CLI developer
- I want to load settings from appsettings.json and environment-specific overrides
- So that I can control behavior without recompiling

### Acceptance Criteria

- [ ] SHALL load configuration from appsettings.json first, THEN appsettings.{Environment}.json (if present), THEN environment variables; WHERE environment is taken from DOTNET_ENVIRONMENT or defaults to Production.
- [ ] WHEN a key exists in multiple sources, THEN the last source in the pipeline overrides previous values.
- [ ] IF no files are present, THEN the application still builds a configuration from environment variables only without error.

## User Story 2

- As a command implementer
- I want typed access to key settings (HTTP timeout, log level, environment)
- So that I can read validated values easily in handlers

### Acceptance Criteria

- [ ] SHALL provide a typed POCO (AppConfig) bound from configuration with properties: Environment (string), HttpTimeoutSeconds (int), LogLevel (string).
- [ ] WHEN a value is missing, THEN defaults apply: Environment="Production"; HttpTimeoutSeconds=30; LogLevel="Information".
- [ ] IF a provided value is invalid (e.g., HttpTimeoutSeconds <= 0 or > 300), THEN the binding/normalization clamps to default 30 without throwing.

## User Story 3

- As a maintainer
- I want environment variable overrides
- So that CI or runtime environments can adjust behavior easily

### Acceptance Criteria

- [ ] SHALL read environment variables without prefix for simplicity (e.g., App__HttpTimeoutSeconds=10 using double-underscore for nesting).
- [ ] WHEN environment variables are set, THEN they override file values.
- [ ] WHILE running in Development, WHERE appsettings.Development.json exists, THEN those values are applied before env vars.

> End of Feature Specification for F3.1, last updated 2025-08-28.
