using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;

namespace ExceptionsHierarchy;

[ExcludeFromCodeCoverage]
public sealed partial class ApplicationLogger(ILogger logger, string roleName)
{
    public void LogException([NotNull] InterviewBaseException exception, [CallerMemberName] string methodName = default!)
    {
        if (logger.IsEnabled(LogLevel.Error) || logger.IsEnabled(LogLevel.Critical))
        {
            var logStateException = new LogStateException(roleName, methodName, exception);
            logger.Log(DetermineLogLevel(exception.SeverityLevel), exception.EventId, logStateException, exception, LogStateException.Format);
        }
    }

    public void LogException([NotNull] Exception exception, string correlationId, [CallerMemberName] string methodName = default!)
    {
        if (logger.IsEnabled(LogLevel.Error) || logger.IsEnabled(LogLevel.Critical))
        {
            logger.LogCritical(exception, "Message: {Message}, Method Name: {MethodName}, CorrelationId: {CorrelationId}",
                exception.Message,
                methodName,
                correlationId);
        }
    }

    ////public void LogInformation(LogLevel logLevel, LogEvent logEvent, string messageTemplate, [CallerMemberName] string methodName = default!)
    ////{
    ////    if (logger.IsEnabled(LogLevel.Information))
    ////    {
    ////       logger.Log(logLevel, new EventId((int)logEvent, logEvent.ToString()), messageTemplate, )
    ////    }
    ////}

    private static LogLevel DetermineLogLevel(SeverityLevel severityLevel)
    {
        return severityLevel switch
        {
            SeverityLevel.Critical => LogLevel.Critical,
            SeverityLevel.Error => LogLevel.Error,
            SeverityLevel.Warning => LogLevel.Warning,
            _ => LogLevel.Critical,
        };
    }
}