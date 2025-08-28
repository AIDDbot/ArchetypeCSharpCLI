# Feature Specification — F1.2 Command routing and option validation

Status: ⛪ SPECIFIED

## Summary

Define a minimal pattern for subcommands, options, and basic validation using System.CommandLine. Provide at least one sample subcommand and demonstrate handler mapping and predictable exit behavior without introducing external dependencies.

## User Stories

1) As a developer, I want the CLI to route to subcommands so that I can add features without modifying the root behavior.

2) As a developer, I want options to be validated (required/non-empty/range) so that invalid input fails fast with clear messages and non-zero exit codes.

3) As a user, I want helpful usage text for each subcommand so that I can discover options quickly.

## Acceptance Criteria (EARS)

Story 1 — Command routing
- SHALL: The CLI SHALL support at least one subcommand with its own description and options.
- WHEN: WHEN a valid subcommand is invoked, THEN its handler SHALL execute and return exit code 0 on success.
- IF: IF an unknown subcommand is passed, THEN the parser SHALL report an error and exit non‑zero.

Story 2 — Option validation
- SHALL: Options SHALL be validated for required/arity and simple business rules (non-empty, numeric range) at parse time.
- WHEN: WHEN validation fails, THEN the CLI SHALL print an actionable message and exit non‑zero without running the handler.
- WHILE: WHILE help is requested, validation SHALL not block help output.

Story 3 — Help and discoverability
- SHALL: Each subcommand SHALL display its own usage and options in `--help`.
- WHEN: WHEN no arguments are provided, THEN the root help SHALL be shown (existing behavior preserved).
- WHERE: WHERE a short alias is reasonable (e.g., `-n` for `--name`), it SHALL be provided.

## Scope and Constraints

- In scope: Root command wiring, at least one example subcommand (e.g., `hello --name`), basic validators, handler mapping, and stable exit codes for success vs. validation errors.
- Out of scope: Configuration, DI, HTTP, weather command, and advanced error policy (covered by later features).

## Non‑Functional Requirements

- Cross‑platform behavior on .NET 9.
- Keep implementation minimal and idiomatic. No breaking changes to existing help/version tests.

## Notes

- This feature unblocks later weather command work by establishing the routing/validation pattern.

> End of F1.2 Specification.
