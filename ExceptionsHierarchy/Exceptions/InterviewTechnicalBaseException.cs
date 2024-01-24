using System.Net;
using Microsoft.ApplicationInsights.DataContracts;

namespace ExceptionsHierarchy;

public abstract class InterviewTechnicalBaseException : InterviewBaseException
{
    protected InterviewTechnicalBaseException(string message, string messageId, LogEvent logEvent) : base(message, messageId, logEvent) { }
    protected InterviewTechnicalBaseException(string message, string messageId, LogEvent logEvent, Exception inner) : base(message, messageId, logEvent, inner) { }

    public override SeverityLevel SeverityLevel => SeverityLevel.Critical;
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}
