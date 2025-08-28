# Implementation Plan

## Overview

Add a centralized ExitCodes contract, wire CommandFactory middleware to set exit codes for parse/unhandled errors, and align HttpErrorHandler mappings with these constants.

## Context

- /docs/backlog/feat-exit-codes.spec.md
- /docs/backlog/feat-exit-codes.design.md
- /.github/instructions/lng_csharp.instructions.md
- /docs/STRUCTURE.md

## Tasks

- [ ] 1. Create central ExitCodes contract
  - Add `src/ArchetypeCSharpCLI/ExitCodes.cs` with documented constants (0..4)
  - Ensure namespace `ArchetypeCSharpCLI`

- [ ] 2. Wire CLI host middleware for parse/unhandled mapping
  - Update `Commands/CommandFactory.cs` to check `context.ParseResult.Errors`
  - If any, set `context.ExitCode = ExitCodes.ValidationOrClientError` and return
  - Wrap `await next(context)` with try/catch and set `ExitCodes.Unexpected` on unhandled

- [ ] 3. Align HttpErrorHandler with ExitCodes
  - Update `Http/IHttpErrorHandler.cs` implementation to use ExitCodes constants
  - Keep messages concise and actionable

- [ ] 4. Keep existing behavior of HttpClientExtensions
  - No changes required; it throws with messages produced by handler

- [ ] 5. Update BACKLOG status to ✨ CODED after implementation

> End of Feature Implementation Tasks for feat-exit-codes, last updated 2025-08-28.
