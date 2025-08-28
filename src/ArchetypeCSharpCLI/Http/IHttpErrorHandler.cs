using System.Net;
using Microsoft.Extensions.Logging;

namespace ArchetypeCSharpCLI.Http;

/// <summary>
/// Interface for handling HTTP errors and mapping them to user-friendly messages.
/// </summary>
public interface IHttpErrorHandler
{
    /// <summary>
    /// Handles an exception that occurred during an HTTP request.
    /// </summary>
    HttpError HandleException(Exception exception);

    /// <summary>
    /// Handles an HTTP response with non-2xx status code.
    /// </summary>
    HttpError HandleHttpResponse(HttpResponseMessage response);
}

/// <summary>
/// Default implementation of HTTP error handling.
/// </summary>
public class HttpErrorHandler : IHttpErrorHandler
{
    private readonly ILogger<HttpErrorHandler> _logger;

    public HttpErrorHandler(ILogger<HttpErrorHandler> logger)
    {
        _logger = logger;
    }

    public HttpError HandleException(Exception exception)
    {
        var correlationId = Guid.NewGuid().ToString("N");

        _logger.LogDebug(exception, "HTTP request failed with exception. Correlation ID: {CorrelationId}", correlationId);

        return exception switch
        {
            TaskCanceledException or OperationCanceledException => new HttpError(
                HttpErrorType.Timeout,
                "Request timed out. The server took too long to respond. Try increasing the timeout with --timeout or check your network connection.",
                1,
                exception,
                correlationId),

            HttpRequestException httpEx when httpEx.InnerException is System.Net.Sockets.SocketException => new HttpError(
                HttpErrorType.Network,
                "Network connection failed. Please check your internet connection and try again.",
                1,
                exception,
                correlationId),

            HttpRequestException => new HttpError(
                HttpErrorType.Network,
                "HTTP request failed. Please check your network connection and the target URL.",
                1,
                exception,
                correlationId),

            _ => new HttpError(
                HttpErrorType.Unexpected,
                "An unexpected error occurred while making the HTTP request.",
                4,
                exception,
                correlationId)
        };
    }

    public HttpError HandleHttpResponse(HttpResponseMessage response)
    {
        var correlationId = Guid.NewGuid().ToString("N");

        _logger.LogDebug("HTTP response received with status {StatusCode} {ReasonPhrase}. Correlation ID: {CorrelationId}",
            (int)response.StatusCode, response.ReasonPhrase, correlationId);

        var (errorType, exitCode) = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => (HttpErrorType.ClientError, 2),
            HttpStatusCode.Forbidden => (HttpErrorType.ClientError, 2),
            HttpStatusCode.NotFound => (HttpErrorType.ClientError, 2),
            HttpStatusCode.BadRequest => (HttpErrorType.ClientError, 2),
            HttpStatusCode.TooManyRequests => (HttpErrorType.ClientError, 2),
            _ when (int)response.StatusCode >= 400 && (int)response.StatusCode < 500 => (HttpErrorType.ClientError, 2),
            _ when (int)response.StatusCode >= 500 => (HttpErrorType.ServerError, 3),
            _ => (HttpErrorType.Unexpected, 4)
        };

        var message = errorType switch
        {
            HttpErrorType.ClientError => $"Request failed with status {response.StatusCode}. Please check your request parameters and try again.",
            HttpErrorType.ServerError => $"Server error occurred (status {response.StatusCode}). The service may be temporarily unavailable. Please try again later.",
            _ => $"Unexpected HTTP response (status {response.StatusCode})."
        };

        return new HttpError(errorType, message, exitCode, null, correlationId);
    }
}
