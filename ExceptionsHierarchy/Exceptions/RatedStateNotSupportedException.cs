namespace ExceptionsHierarchy;

public sealed class RatedStateNotSupportedException : InterviewBusinessCriticalBaseException
{
    public RatedStateNotSupportedException(ExceptionData exceptionData, LogEvent logEvent = LogEvent.OnRatedStateValidation) : base(exceptionData, logEvent) { }

    public RatedStateNotSupportedException(ExceptionData exceptionData, LogEvent logEvent, Exception inner) : base(exceptionData, logEvent, inner) { }

    public override string Reason => "The Rated State is not Supported/Turned on yet";
}
