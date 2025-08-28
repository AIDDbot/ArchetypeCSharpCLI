# Implementation Plan

## Overview

Introduce an `ErrorOutput` helper and update `CommandFactory` to print concise error messages for parse/unhandled cases. Preserve existing HTTP error message formatting via `HttpError.ToString()`.

## Context

- /docs/backlog/feat-error-messages.spec.md
- /docs/backlog/feat-error-messages.design.md
- /.github/instructions/lng_csharp.instructions.md

## Tasks

- [x] 1. Add ErrorOutput helper
  - Create `src/ArchetypeCSharpCLI/ErrorOutput.cs`
  - Implement `Write(string message, string? correlationId = null)` and `Write(Http.HttpError err)`

- [x] 2. Update CommandFactory middleware
  - On parse errors, write `Error: <message>` from first parse error to stderr and set exit code
  - On unhandled exceptions, write `Error: An unexpected error occurred.` to stderr and set exit code

- [x] 3. Optional verbosity
  - Read env var `AIDDBOT_VERBOSE`; if `1`, ensure logs include Debug level (existing logging already configured via providers)

- [x] 4. Update BACKLOG status to ✨ CODED after implementation

> End of Feature Implementation Tasks for feat-error-messages, last updated 2025-08-28.
