# Implementation Plan — F1.2 Command routing and option validation

## Overview

Add a simple command routing pattern with a sample `hello` command and input validation using System.CommandLine.

## Context

- System.CommandLine 2.0 beta
- C# guidelines in `.github/instructions/lng_csharp.instructions.md`
- Design: `feat-command-routing.design.md`

## Tasks

- [ ] 1. Create command scaffolding
  - Add `Commands/CommandFactory.cs` building root and subcommands
  - Move parser construction out of `Program` to `CommandFactory`
  - Keep `--help`/`--version` behavior intact
- [ ] 2. Implement `hello` subcommand
  - Create `Commands/Hello/HelloOptions.cs`
  - Create `Commands/Hello/HelloCommandHandler.cs`
  - Wire `hello` command with `--name|-n` option (required)
  - Add validator to reject empty/whitespace `name`
- [ ] 3. Integrate in Program
  - Replace `Program.BuildParser()` to call `CommandFactory.BuildParser()`
  - Verify default help on no args still works
- [ ] 4. Smoke test locally
  - Build the solution
  - Run `archetype --help`, `archetype hello --name Test`

> End of Feature Implementation Tasks for F1.2, last updated 2025-08-28.
