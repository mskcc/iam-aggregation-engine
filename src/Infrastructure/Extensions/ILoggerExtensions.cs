using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;

public static class LoggingExtensions
{
    /// <summary>
    /// Logs at the information log levelto the configured SQL server instance.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logMessage"></param>
    /// <param name="args"></param>
    public static void LogInformationToSql<T>(this ILogger<T> logger, string logMessage, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        using (logger.BeginScope(new Dictionary<string, object> { { LoggerContexts.SqlLogger, true } }))
        {
            if (args is null)
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
    public static void LogWarningToSql<T>(this ILogger<T> logger, string logMessage, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        using (logger.BeginScope(new Dictionary<string, object> { { LoggerContexts.SqlLogger, true } }))
        {
                        if (args is null)
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
    public static void LogDebugToSql<T>(this ILogger<T> logger, string logMessage, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        using (logger.BeginScope(new Dictionary<string, object> { { LoggerContexts.SqlLogger, true } }))
        {
            if (args is null)
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
    public static void LogErrorToSql<T>(this ILogger<T> logger, string logMessage, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        using (logger.BeginScope(new Dictionary<string, object> { { LoggerContexts.SqlLogger, true } }))
        {
            if (args is null)
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
    public static void LogCriticalToSql<T>(this ILogger<T> logger, string logMessage, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        using (logger.BeginScope(new Dictionary<string, object> { { LoggerContexts.SqlLogger, true } }))
        {
            if (args is null)
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
    public static void LogTraceToSql<T>(this ILogger<T> logger, string logMessage, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        using (logger.BeginScope(new Dictionary<string, object> { { LoggerContexts.SqlLogger, true } }))
        {
            if (args is null)
            {
                logger.LogTrace(logMessage);
                return;
            }

            logger.LogTrace(logMessage, args);
        }
    }
}