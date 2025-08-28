# feat-dto-mapping DTOs and mapping for external APIs Specification

Define minimal DTOs for ip-api.com and open-meteo.com and map to internal models.

- **Epic**: E4
- **Priority**: ❗ High
- **Product Requirements**: 
  - R2 Weather Command

## User Story 1

- **As a** developer using the CLI
- **I want to** define DTOs for external API responses
- **So that** I can deserialize JSON responses into strongly-typed objects

### Acceptance Criteria

- [ ] SHALL define IpApiResponse DTO for ip-api.com with latitude, longitude, city, country fields
- [ ] SHALL define OpenMeteoResponse DTO for open-meteo.com with temperature, weather code, and observation time
- [ ] SHALL use System.Text.Json attributes for proper deserialization
- [ ] SHALL include only the fields needed for the weather command
- [ ] WHEN deserializing JSON, THEN handle missing fields gracefully with nullable types

## User Story 2

- **As a** developer building the weather command
- **I want to** map external DTOs to internal domain models
- **So that** application logic doesn't depend on external API structures

### Acceptance Criteria

- [ ] SHALL create mapper classes to convert IpApiResponse to Location
- [ ] SHALL create mapper classes to convert OpenMeteoResponse to WeatherReport
- [ ] SHALL handle unit conversions (Celsius to Fahrenheit) in the mapper
- [ ] SHALL validate required fields and throw meaningful exceptions for invalid data
- [ ] WHEN mapping weather data, THEN convert weather codes to human-readable descriptions

## User Story 3

- **As a** developer maintaining the code
- **I want to** minimal DTOs that only include needed fields
- **So that** the code is maintainable and performant

### Acceptance Criteria

- [ ] SHALL include only latitude, longitude, city, country from ip-api.com
- [ ] SHALL include only current temperature, weather code, and time from open-meteo.com
- [ ] SHALL not include unused fields like timezone, elevation, or forecast data
- [ ] WHEN the API adds new fields, THEN existing DTOs should continue to work
- [ ] SHALL document the purpose and source of each DTO field

> End of Feature Specification for feat-dto-mapping, last updated 2025-08-28.</content>
<parameter name="filePath">c:\code\aidd\ArchetypeCSharpCLI\docs\backlog\feat-dto-mapping.spec.md
