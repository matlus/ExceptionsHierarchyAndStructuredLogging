namespace ExceptionsHierarchy;

public sealed class RatedStateNotSupportedException : InterviewBusinessCriticalBaseException
{
    public RatedStateNotSupportedException(string message, string messageId, LogEvent logEvent = LogEvent.OnRatedStateValidation) : base(message, messageId, logEvent) { }

    public RatedStateNotSupportedException(string message, string messageId, LogEvent logEvent, Exception inner) : base(message, messageId, logEvent, inner) { }

    public override string Reason => "The Rated State is not Supported/Turned on yet";
}
