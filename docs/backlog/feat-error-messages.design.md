# feat-error-messages User-friendly error messages Design 

## Overview

Standardize human-facing error output on stderr while keeping technical details in structured logs. Reuse existing HttpError.ToString for message shape and add top-level handler for unhandled exceptions.

## Data Models

### Error message shape

- Purpose: Human-facing single-line error summary (Output)
- Layer: Host/Commands

```
Error: <message> (Correlation ID: <id>)
```

## Components

### ErrorOutput

- Purpose: Central helper to write concise error messages to stderr
- Interfaces: `static void Write(Http.HttpError err)` and `static void Write(string message, string? correlationId = null)`
- Dependencies: None
- Reuses: HttpError.ToString

### Command Host Middleware (update)

- Purpose: When mapping parse errors or unhandled exceptions, write concise stderr message once
- Interfaces: System.CommandLine middleware
- Behavior: On parse errors, print the first error message in a single line prefixed with `Error:`; on unhandled exceptions, print `Error: An unexpected error occurred.` with no stack trace.

### Verbose diagnostics

- Purpose: Enable detailed logs via env var AIDDBOT_VERBOSE=1
- Behavior: When present, set minimum log level to Debug; keep stderr unchanged.

## User interface

No changes to commands or options.

## Aspects

### Monitoring

- Logs contain exception details. Stderr stays concise.

### Security

- Avoid leaking sensitive data in stderr by default.

### Error Handling

- First failure wins; print only one concise line.

## Architecture

Small helper class and minor middleware changes.

### File Structure

```
src/ArchetypeCSharpCLI/
  ErrorOutput.cs                 # helper
  Commands/CommandFactory.cs     # print concise messages on errors
```

> End of Feature Design for feat-error-messages, last updated 2025-08-28.
