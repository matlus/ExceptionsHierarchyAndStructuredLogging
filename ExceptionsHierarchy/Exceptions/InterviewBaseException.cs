using System.Net;
using Microsoft.Extensions.Logging;

namespace ExceptionsHierarchy;

public abstract class InterviewBaseException : Exception
{
    protected InterviewBaseException(ExceptionData exceptionData, LogEvent logEvent) :this(exceptionData, logEvent, default!) { }
    protected InterviewBaseException(ExceptionData exceptionData, LogEvent logEvent, Exception inner)
        : base(exceptionData.Message, inner)
    {
        ExceptionData = exceptionData;
        EventId = new EventId((int)logEvent, logEvent.ToString());
    }

    public EventId EventId { get; }
    public abstract ExceptionCategory ExceptionCategory { get; }
    public abstract string Reason { get; }
    public abstract HttpStatusCode StatusCode { get; }
    public ExceptionData ExceptionData { get; init; }
}