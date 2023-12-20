namespace ExceptionsHierarchy;

public abstract class InterviewBusinessBaseException : InterviewBaseException
{
    protected InterviewBusinessBaseException(ExceptionData exceptionData, LogEvent logEvent) : base(exceptionData, logEvent) { }
    protected InterviewBusinessBaseException(ExceptionData exceptionData, LogEvent logEvent, Exception inner) : base(exceptionData, logEvent, inner) { }

    public override ExceptionCategory ExceptionCategory => ExceptionCategory.BusinessError;
}
