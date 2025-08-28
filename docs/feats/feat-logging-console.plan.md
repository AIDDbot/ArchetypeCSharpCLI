# Implementation Plan

## Overview

Add console logging with Microsoft.Extensions.Logging, wire minimal scopes in middleware and handler, configure from AppSettings.LogLevel.

## Context

- Microsoft.Extensions.Logging.Console
- AppSettings (ConfigBuilder, AppConfig)

## Tasks

- [ ] 1. Add logging packages and helper
  - Add Microsoft.Extensions.Logging and Console provider packages
  - Create `Logging/Log.cs` with factory reading AppSettings.LogLevel
- [ ] 2. Wire root middleware scope
  - In CommandFactory, add using and BeginScope with command and args
- [ ] 3. Add sample logs in hello handler
  - Create logger and write a Debug line inside a scope
- [ ] 4. Verify defaults and configuration
  - Ensure default level Information, invalid values fallback to Information

> End of Feature Implementation Tasks for F3.2, last updated 2025-08-28.
