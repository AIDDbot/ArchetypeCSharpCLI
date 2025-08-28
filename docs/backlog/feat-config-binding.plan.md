# Implementation Plan

## Overview

Implement a minimal options binding facility on top of the existing configuration pipeline to support typed POCO binding, validation, and live reload, without introducing full DI yet.

## Context

- Architecture Instructions: .github/instructions/bst_architecture.instructions.md
- STRUCTURE.md
- Spec: docs/backlog/feat-config-binding.spec.md
- Design: docs/backlog/feat-config-binding.design.md

## Tasks

- [x] 1. Update configuration reload behavior
  - Enable `reloadOnChange: true` in `ConfigBuilder` JSON providers
  - Keep precedence and environment detection intact
  - Leave environment variables provider as-is

- [x] 2. Add binding infrastructure folder and files
  - Create folder: `src/ArchetypeCSharpCLI/Configuration/Binding/`
  - Add `OptionsBootstrap.cs`: static bootstrap to create `ServiceCollection`, add `Options`, store `IServiceProvider`
  - Add `OptionsExtensions.cs`: `AddBoundOptions<T>(...)` helper with `Bind`, `ValidateDataAnnotations`, and optional custom validation
  - Add `OptionsAccess.cs`: static accessors `Get<T>()` and `GetMonitor<T>()` resolving from stored `IServiceProvider`

- [x] 3. Wire bootstrap at startup (non-invasive)
  - In `Program.Main`, obtain `IConfigurationRoot` via `ConfigBuilder.BuildRaw()`
  - Call `OptionsBootstrap.Init(rawConfig)`; no bound types by default
  - Keep existing `AppSettings` usage unchanged

- [x] 4. Add package references (if missing)
  - Add `Microsoft.Extensions.Options`
  - Add `Microsoft.Extensions.Options.ConfigurationExtensions`
  - Add `Microsoft.Extensions.Options.DataAnnotations`

- [x] 5. Code quality and consistency
  - Use `sealed` classes for POCOs and static classes for helpers
  - Respect nullable annotations and existing style
  - Keep APIs internal where possible; expose minimal surface area

> End of Feature Implementation Tasks for F3.3, last updated 2025-08-28.
