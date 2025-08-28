# Implementation Plan

## Overview

Implement a minimal configuration pipeline using Microsoft.Extensions.Configuration, bind to a typed AppConfig, expose via AppSettings, and initialize on startup. Keep DI/logging for later features.

## Context

- .github/instructions/tpl_feature-plan.instructions.md
- .github/instructions/lng_csharp.instructions.md
- docs/backlog/feat-config-pipeline.spec.md
- docs/backlog/feat-config-pipeline.design.md

## Tasks

- [ ] 1. Add configuration packages to project
  - Update ArchetypeCSharpCLI.csproj with Microsoft.Extensions.Configuration, Binder, Json, EnvironmentVariables
- [ ] 2. Add appsettings files
  - Create src/ArchetypeCSharpCLI/appsettings.json with defaults
  - Ensure files copy to output on build
- [ ] 3. Implement Configuration classes
  - Create AppConfig.cs POCO with defaults
  - Create ConfigBuilder.cs to build IConfigurationRoot and bind AppConfig with clamping
  - Create AppSettings.cs to cache Current config (lazy init)
- [ ] 4. Initialize configuration on application startup
  - Update Program.cs to touch AppSettings.Current early
- [ ] 5. Smoke test
  - Build and run --version/--help to ensure no regressions

> End of Feature Implementation Tasks for F3.1, last updated 2025-08-28.
