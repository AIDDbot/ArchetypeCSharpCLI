using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ArchetypeCSharpCLI.Http;

/// <summary>
/// Extension methods for HttpClient with comprehensive error handling.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Sends a GET request and deserializes the JSON response with error handling.
    /// </summary>
    public static async Task<T?> GetFromJsonWithErrorHandlingAsync<T>(
        this HttpClient client,
        string requestUri,
        IHttpErrorHandler errorHandler,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var correlationId = Guid.NewGuid().ToString("N");
            logger.LogDebug("Sending GET request to {Uri}. Correlation ID: {CorrelationId}", requestUri, correlationId);

            var response = await client.GetAsync(requestUri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var httpError = errorHandler.HandleHttpResponse(response);
                // Update correlation ID to match the error handler's
                var errorWithCorrelation = new HttpError(
                    httpError.Type,
                    httpError.Message,
                    httpError.ExitCode,
                    httpError.Exception,
                    correlationId);

                logger.LogError("HTTP request failed: {Error}", errorWithCorrelation.ToString());
                throw new HttpRequestException(errorWithCorrelation.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }

            logger.LogDebug("HTTP request successful. Correlation ID: {CorrelationId}", correlationId);
            return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
        }
        catch (Exception ex) when (ex is not HttpRequestException)
        {
            var httpError = errorHandler.HandleException(ex);
            logger.LogError("HTTP request failed: {Error}", httpError.ToString());
            throw new HttpRequestException(httpError.Message, ex, System.Net.HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Sends a GET request and returns the response with error handling.
    /// </summary>
    public static async Task<HttpResponseMessage> GetWithErrorHandlingAsync(
        this HttpClient client,
        string requestUri,
        IHttpErrorHandler errorHandler,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var correlationId = Guid.NewGuid().ToString("N");
            logger.LogDebug("Sending GET request to {Uri}. Correlation ID: {CorrelationId}", requestUri, correlationId);

            var response = await client.GetAsync(requestUri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var httpError = errorHandler.HandleHttpResponse(response);
                // Update correlation ID to match the error handler's
                var errorWithCorrelation = new HttpError(
                    httpError.Type,
                    httpError.Message,
                    httpError.ExitCode,
                    httpError.Exception,
                    correlationId);

                logger.LogError("HTTP request failed: {Error}", errorWithCorrelation.ToString());
                throw new HttpRequestException(errorWithCorrelation.Message, null, System.Net.HttpStatusCode.InternalServerError);
            }

            logger.LogDebug("HTTP request successful. Correlation ID: {CorrelationId}", correlationId);
            return response;
        }
        catch (Exception ex) when (ex is not HttpRequestException)
        {
            var httpError = errorHandler.HandleException(ex);
            logger.LogError("HTTP request failed: {Error}", httpError.ToString());
            throw new HttpRequestException(httpError.Message, ex, System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
