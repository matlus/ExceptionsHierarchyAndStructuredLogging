using Microsoft.ApplicationInsights.DataContracts;

namespace ExceptionsHierarchy;

public abstract class InterviewBusinessCriticalBaseException : InterviewBusinessBaseException
{
    protected InterviewBusinessCriticalBaseException(string message, string messageId, LogEvent logEvent) : base(message, messageId, logEvent) { }
    protected InterviewBusinessCriticalBaseException(string message, string messageId, LogEvent logEvent, Exception inner) : base(message, messageId, logEvent, inner) { }

    public override sealed SeverityLevel SeverityLevel => SeverityLevel.Critical;
}

