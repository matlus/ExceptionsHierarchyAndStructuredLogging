using System.Net;
using Microsoft.ApplicationInsights.DataContracts;

namespace ExceptionsHierarchy;

public abstract class InterviewBusinessBaseException : InterviewBaseException
{
    protected InterviewBusinessBaseException(string message, string messageId, LogEvent logEvent) : base(message, messageId, logEvent) { }
    protected InterviewBusinessBaseException(string message, string messageId, LogEvent logEvent, Exception inner) : base(message, messageId, logEvent, inner) { }

    public override SeverityLevel SeverityLevel => SeverityLevel.Error;
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
