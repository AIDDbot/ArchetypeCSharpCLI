# F1.1 CLI host skeleton and help/version Specification

Provide a minimal, reliable CLI host with `--help` and `--version` so developers can run and extend commands confidently.

- **Epic**: E1 CLI Core
- **Priority**: ❗️ Critical 
- **Product Requirements**: 
  
- R1 CLI Host and Basic Commands (from PRD.md)

## User Story 1

- As a developer
- I want to run the CLI with `--help`
- So that I can see usage and available options/commands

### Acceptance Criteria

- [ ] WHEN the user runs the CLI with `--help`, the system SHALL display usage, root description, and options.
- [ ] WHEN no subcommands are implemented, the system SHALL present help for the root command.

## User Story 2

- As a developer
- I want to run the CLI with `--version` (or `-v`)
- So that I can see the application version

### Acceptance Criteria

- [ ] WHEN the user passes `--version` or `-v`, the system SHALL print the semantic version and exit with code 0.
- [ ] IF both `--help` and `--version` are provided, THEN the system SHALL show help (standard precedence) without error.

## User Story 3

- As a developer
- I want the project to build and run cross-platform
- So that I can clone and get started quickly

### Acceptance Criteria

- [ ] The system SHALL build with a supported LTS .NET SDK (8 or 9) and run `dotnet run -- --help` successfully.
- [ ] The system SHALL use a command framework (System.CommandLine) to enable future subcommands and options.

> End of Feature Specification for F1.1, last updated 2025-08-28.
