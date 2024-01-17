using System.Net;

namespace ExceptionsHierarchy;

public abstract class InterviewTechnicalBaseException : InterviewBaseException
{
    protected InterviewTechnicalBaseException(ExceptionData exceptionData, LogEvent logEvent) : base(exceptionData, logEvent) { }
    protected InterviewTechnicalBaseException(ExceptionData exceptionData, LogEvent logEvent, Exception inner) : base(exceptionData, logEvent, inner) { }

    public override ExceptionCategory ExceptionCategory => ExceptionCategory.Technical;
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}
