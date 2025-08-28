# Implementation Plan

## Overview

Implement DTOs for external weather APIs and mapping logic to convert them to internal domain models, ensuring clean separation and maintainable code.

## Context

- C# language with .NET 9
- System.Text.Json for JSON deserialization
- Microsoft.Extensions.DependencyInjection for DI
- Domain models: Location, WeatherReport

## Tasks

- [ ] 1. Create DTO classes
  - Define IpApiResponse with status, lat, lon, city, country fields
  - Define OpenMeteoResponse with current weather data
  - Add JsonPropertyName attributes for proper deserialization
  - Use nullable types for optional fields

- [ ] 2. Implement WeatherCodeMapper
  - Create static mapping from WMO weather codes to descriptions
  - Handle common weather codes (clear, cloudy, rain, snow, etc.)
  - Provide fallback for unknown codes

- [ ] 3. Create IpApiMapper implementation
  - Implement IIpApiMapper interface
  - Validate required fields (lat, lon)
  - Map IpApiResponse to Location domain model
  - Handle validation errors with descriptive exceptions

- [ ] 4. Create OpenMeteoMapper implementation
  - Implement IOpenMeteoMapper interface
  - Map temperature with unit conversion (Celsius/Fahrenheit)
  - Convert weather codes to human-readable descriptions
  - Parse observation time and validate data

- [ ] 5. Register mappers in DI container
  - Add mapper registrations to HttpServiceCollectionExtensions
  - Register as singleton services
  - Ensure WeatherCodeMapper is available for injection

- [ ] 6. Add temperature unit conversion utilities
  - Create extension methods for Celsius to Fahrenheit conversion
  - Handle precision and rounding appropriately
  - Support both directions if needed

> End of Feature Implementation Tasks for feat-dto-mapping, last updated 2025-08-28.</content>
<parameter name="filePath">c:\code\aidd\ArchetypeCSharpCLI\docs\backlog\feat-dto-mapping.plan.md
