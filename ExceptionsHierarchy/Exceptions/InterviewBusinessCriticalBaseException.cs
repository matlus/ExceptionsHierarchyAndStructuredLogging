namespace ExceptionsHierarchy;

public abstract class InterviewBusinessCriticalBaseException : InterviewBusinessBaseException
{
    protected InterviewBusinessCriticalBaseException(ExceptionData exceptionData, LogEvent logEvent) : base(exceptionData, logEvent) { }
    protected InterviewBusinessCriticalBaseException(ExceptionData exceptionData, LogEvent logEvent, Exception inner) : base(exceptionData, logEvent, inner) { }

    public override sealed ExceptionCategory ExceptionCategory => ExceptionCategory.BusinessCritical;
}

