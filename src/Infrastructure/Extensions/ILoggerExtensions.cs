using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Serilog.Context;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;

public static class LoggingExtensions
{
    /// <summary>
    /// Logs at the information log levelto the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogInformationToSql<T>(this ILogger<T> logger,
        string logMessage,
        object?[]? args = null,
        string? requestId = null,
        string? pingOneUserId = null,
        string? environment = null,
        string? status = null,
        string? detail = null,
        string? error = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var scopeData = new Dictionary<string, object>
        {
            { LoggerContexts.SqlLogger, true }
        };

        if (!string.IsNullOrEmpty(requestId)) scopeData["RequestId"] = requestId;
        if (!string.IsNullOrEmpty(pingOneUserId)) scopeData["PingOneUserId"] = pingOneUserId;
        if (!string.IsNullOrEmpty(environment)) scopeData["Environment"] = environment;
        if (!string.IsNullOrEmpty(status)) scopeData["Status"] = status;
        if (!string.IsNullOrEmpty(detail)) scopeData["Detail"] = detail;
        if (!string.IsNullOrEmpty(error)) scopeData["Error"] = error;

        using (logger.BeginScope(scopeData))
        {
            if (args == null || args.Length == 0)
            {
                logger.LogInformation(logMessage);
                return;
            }

            logger.LogInformation(logMessage, args);
        }
    }

    /// <summary>
    /// Logs at the warning log level to the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogWarningToSql<T>(this ILogger<T> logger,
        string logMessage,
        object?[]? args = null,
        string? requestId = null,
        string? pingOneUserId = null,
        string? environment = null,
        string? status = null,
        string? detail = null,
        string? error = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var scopeData = new Dictionary<string, object>
        {
            { LoggerContexts.SqlLogger, true }
        };

        if (!string.IsNullOrEmpty(requestId)) scopeData["RequestId"] = requestId;
        if (!string.IsNullOrEmpty(pingOneUserId)) scopeData["PingOneUserId"] = pingOneUserId;
        if (!string.IsNullOrEmpty(environment)) scopeData["Environment"] = environment;
        if (!string.IsNullOrEmpty(status)) scopeData["Status"] = status;
        if (!string.IsNullOrEmpty(detail)) scopeData["Detail"] = detail;
        if (!string.IsNullOrEmpty(error)) scopeData["Error"] = error;

        using (logger.BeginScope(scopeData))
        {
            if (args == null || args.Length == 0)
            {
                logger.LogWarning(logMessage);
                return;
            }

            logger.LogWarning(logMessage, args);
        }
    }

    /// <summary>
    /// Logs at the debug log level to the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogDebugToSql<T>(this ILogger<T> logger,
        string logMessage,
        object?[]? args = null,
        string? requestId = null,
        string? pingOneUserId = null,
        string? environment = null,
        string? status = null,
        string? detail = null,
        string? error = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var scopeData = new Dictionary<string, object>
        {
            { LoggerContexts.SqlLogger, true }
        };

        if (!string.IsNullOrEmpty(requestId)) scopeData["RequestId"] = requestId;
        if (!string.IsNullOrEmpty(pingOneUserId)) scopeData["PingOneUserId"] = pingOneUserId;
        if (!string.IsNullOrEmpty(environment)) scopeData["Environment"] = environment;
        if (!string.IsNullOrEmpty(status)) scopeData["Status"] = status;
        if (!string.IsNullOrEmpty(detail)) scopeData["Detail"] = detail;
        if (!string.IsNullOrEmpty(error)) scopeData["Error"] = error;

        using (logger.BeginScope(scopeData))
        {
            if (args == null || args.Length == 0)
            {
                logger.LogDebug(logMessage);
                return;
            }

            logger.LogDebug(logMessage, args);
        }
    }

    /// <summary>
    /// Logs at the error log level to the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogErrorToSql<T>(this ILogger<T> logger,
        string logMessage,
        object?[]? args = null,
        string? requestId = null,
        string? pingOneUserId = null,
        string? environment = null,
        string? status = null,
        string? detail = null,
        string? error = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var scopeData = new Dictionary<string, object>
        {
            { LoggerContexts.SqlLogger, true }
        };

        if (!string.IsNullOrEmpty(requestId)) scopeData["RequestId"] = requestId;
        if (!string.IsNullOrEmpty(pingOneUserId)) scopeData["PingOneUserId"] = pingOneUserId;
        if (!string.IsNullOrEmpty(environment)) scopeData["Environment"] = environment;
        if (!string.IsNullOrEmpty(status)) scopeData["Status"] = status;
        if (!string.IsNullOrEmpty(detail)) scopeData["Detail"] = detail;
        if (!string.IsNullOrEmpty(error)) scopeData["Error"] = error;

        using (logger.BeginScope(scopeData))
        {
            if (args == null || args.Length == 0)
            {
                logger.LogError(logMessage);
                return;
            }

            logger.LogError(logMessage, args);
        }
    }

    /// <summary>
    /// Logs at the critical log level to the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogCriticalToSql<T>(this ILogger<T> logger,
        string logMessage,
        object?[]? args = null,
        string? requestId = null,
        string? pingOneUserId = null,
        string? environment = null,
        string? status = null,
        string? detail = null,
        string? error = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var scopeData = new Dictionary<string, object>
        {
            { LoggerContexts.SqlLogger, true }
        };

        if (!string.IsNullOrEmpty(requestId)) scopeData["RequestId"] = requestId;
        if (!string.IsNullOrEmpty(pingOneUserId)) scopeData["PingOneUserId"] = pingOneUserId;
        if (!string.IsNullOrEmpty(environment)) scopeData["Environment"] = environment;
        if (!string.IsNullOrEmpty(status)) scopeData["Status"] = status;
        if (!string.IsNullOrEmpty(detail)) scopeData["Detail"] = detail;
        if (!string.IsNullOrEmpty(error)) scopeData["Error"] = error;

        using (logger.BeginScope(scopeData))
        {
            if (args == null || args.Length == 0)
            {
                logger.LogCritical(logMessage);
                return;
            }

            logger.LogCritical(logMessage, args);
        }
    }

    /// <summary>
    /// Logs at the trace log level to the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogTraceToSql<T>(this ILogger<T> logger,
        string logMessage,
        object?[]? args = null,
        string? requestId = null,
        string? pingOneUserId = null,
        string? environment = null,
        string? status = null,
        string? detail = null,
        string? error = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var scopeData = new Dictionary<string, object>
        {
            { LoggerContexts.SqlLogger, true }
        };

        if (!string.IsNullOrEmpty(requestId)) scopeData["RequestId"] = requestId;
        if (!string.IsNullOrEmpty(pingOneUserId)) scopeData["PingOneUserId"] = pingOneUserId;
        if (!string.IsNullOrEmpty(environment)) scopeData["Environment"] = environment;
        if (!string.IsNullOrEmpty(status)) scopeData["Status"] = status;
        if (!string.IsNullOrEmpty(detail)) scopeData["Detail"] = detail;
        if (!string.IsNullOrEmpty(error)) scopeData["Error"] = error;

        using (logger.BeginScope(scopeData))
        {
            if (args == null || args.Length == 0)
            {
                logger.LogTrace(logMessage);
                return;
            }

            logger.LogTrace(logMessage, args);
        }
    }
}