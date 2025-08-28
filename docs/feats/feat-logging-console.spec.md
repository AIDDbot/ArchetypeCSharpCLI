# F3.2 Structured console logging Specification

Add Microsoft.Extensions.Logging console provider with levels and scopes; ensure helpful defaults.

- Epic: E3 Configuration & Logging
- Priority: ❗ High
- Product Requirements:
  - R3 Configuration and Logging

## User Story 1

- As a CLI user
- I want to see consistent, concise logs with timestamps and levels
- So that I can understand what the tool is doing and diagnose issues

### Acceptance Criteria

- [ ] SHALL output logs to console with timestamp, level, category when log level is enabled.
- [ ] WHEN environment variable or appsettings sets LogLevel, THEN minimum level honors that value; default Information.
- [ ] IF a command executes, THEN log a scope with command name and args; WHILE in scope, logs include the scope values.

## User Story 2

- As an operator
- I want to control verbosity without code changes
- So that I can increase detail for troubleshooting

### Acceptance Criteria

- [ ] WHEN App:LogLevel (or LogLevel) is Debug or Trace, THEN debug/trace messages are printed; otherwise suppressed.
- [ ] WHERE LogLevel is invalid, THEN default to Information.

> End of Feature Specification for F3.2, last updated 2025-08-28.
