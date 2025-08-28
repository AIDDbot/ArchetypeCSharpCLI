# Implementation Plan

## Overview

Implement comprehensive HTTP error handling by creating error models, handler components, and extending the HTTP client infrastructure with proper error mapping and user-friendly messages.

## Context

- C# language with .NET 9
- Microsoft.Extensions.Http for HttpClient
- Microsoft.Extensions.Logging for structured logging
- System.Net.Http for HTTP responses

## Tasks

- [ ] 1. Create HttpError data model
  - Define HttpErrorType enum with Network, Timeout, ClientError, ServerError, Unexpected
  - Create HttpError class with Type, Message, ExitCode, Exception, CorrelationId properties
  - Add ToString() method for user-friendly display

- [ ] 2. Implement IHttpErrorHandler interface
  - Define interface with HandleException and HandleHttpResponse methods
  - Create HttpErrorHandler implementation class
  - Add constructor injection for ILogger

- [ ] 3. Create HttpClientExtensions for error-aware requests
  - Add GetFromJsonWithErrorHandling extension method
  - Handle HttpRequestException and map to HttpError
  - Handle non-2xx responses and map to appropriate HttpError types
  - Log technical details while returning user-friendly errors

- [ ] 4. Register error handling in DI container
  - Update HttpServiceCollectionExtensions to register IHttpErrorHandler
  - Add singleton registration for HttpErrorHandler
  - Ensure ILogger is available for injection

- [ ] 5. Add correlation and logging enhancements
  - Generate correlation IDs for HTTP requests
  - Log request/response details at Debug level
  - Include correlation ID in error messages for tracing

> End of Feature Implementation Tasks for feat-http-error-handling, last updated 2025-08-28.</content>
<parameter name="filePath">c:\code\aidd\ArchetypeCSharpCLI\docs\backlog\feat-http-error-handling.plan.md
