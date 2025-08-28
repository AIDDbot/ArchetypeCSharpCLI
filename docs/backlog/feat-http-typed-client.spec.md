# F4.1 Typed HttpClient with sensible timeouts Specification

Provide a robust, minimal HTTP foundation using IHttpClientFactory with sane defaults and typed client support; enforce a default timeout sourced from configuration to prevent hangs.

- Epic: E4 HTTP & Resilience
- Priority: ❗ High
- Product Requirements:
  - R4 Dependency Injection and HTTP Resilience (from PRD)

## User Story 1

- As a maintainer
- I want a default HttpClient configuration with a sensible timeout
- So that outbound calls do not hang and are consistent across clients

### Acceptance Criteria

- [ ] The CLI SHALL register IHttpClientFactory and enable named/typed clients via DI at startup.
- [ ] WHEN building the default HttpClient, the system SHALL set HttpClient.Timeout to AppConfig.HttpTimeoutSeconds seconds; WHERE the default is 30 if not configured.
- [ ] IF AppConfig.HttpTimeoutSeconds is outside [1, 60], THEN the system SHALL clamp the effective timeout to the nearest bound within [1, 60].

## User Story 2

- As a command implementer
- I want easy registration of typed clients
- So that I can inject API clients without repeating boilerplate

### Acceptance Criteria

- [ ] The CLI SHALL provide an extension method to wire HTTP defaults (e.g., AddHttpCore) that applies to all AddHttpClient registrations.
- [ ] WHEN a typed client is registered via AddHttpClient<TClient, TImpl>, THEN it SHALL inherit the default timeout and headers from the core HTTP configuration.
- [ ] The system SHALL set a default User-Agent header formatted as "ArchetypeCSharpCLI/{Version}" for all HttpClient instances.

## User Story 3

- As an operator
- I want to control HTTP timeout via configuration
- So that I can tune responsiveness per environment

### Acceptance Criteria

- [ ] WHEN AppConfig.HttpTimeoutSeconds is set via configuration files or environment variables, THEN the effective HttpClient timeout SHALL reflect that value (subject to clamping).
- [ ] WHILE no explicit per-request timeout is provided by commands, the system SHALL rely on the HttpClient default timeout set from configuration.
- [ ] WHERE environment-specific files (e.g., appsettings.Development.json) are present, the configured HttpTimeoutSeconds from those files SHALL apply before environment variable overrides.

> End of Feature Specification for F4.1, last updated 2025-08-28.
