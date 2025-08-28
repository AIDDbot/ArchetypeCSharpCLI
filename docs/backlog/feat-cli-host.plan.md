# Implementation Plan

## Overview

Create a .NET console project with System.CommandLine, wire help and version, ensure build/run.

## Context

- Follow: C# instructions (`.github/instructions/lng_csharp.instructions.md`)
- Feature spec/design: `docs/backlog/feat-cli-host.spec.md`, `docs/backlog/feat-cli-host.design.md`

## Tasks

- [x] 1. Scaffold solution and project structure
  - Create `src/ArchetypeCSharpCLI` project as .NET 8 (or 9 if available) console app
  - Add `global.json` to pin SDK version (8 or 9)
  - Add `.editorconfig` basic rules (optional in later tasks)
- [x] 2. Add command framework dependency
  - Add `System.CommandLine` PackageReference
- [x] 3. Implement Program.cs
  - Create root command with description
  - Add `--version` (`-v`) option and handler to print version
  - Ensure default help is enabled
- [x] 4. Build and smoke run
  - `dotnet build`
  - `dotnet run -- --help`

- [x] 5. Add tests (xUnit)
  - Process-level tests for `--help`, no-args default help, `--version`/`-v`, and help precedence
  - Add to solution and ensure `dotnet test` passes

> End of Feature Implementation Tasks for F1.1, last updated 2025-08-28.
