using System.Net;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;

namespace ExceptionsHierarchy;

public abstract class InterviewBaseException : Exception
{
    protected InterviewBaseException(string message, string messageId, LogEvent logEvent) : this(message, messageId, logEvent, default!) { }
    protected InterviewBaseException(string message, string messageId, LogEvent logEvent, Exception inner)
        : base(message, inner)
    {
        MessageId = messageId;
        EventId = new EventId((int)logEvent, logEvent.ToString());
    }

    public string MessageId { get; }
    public EventId EventId { get; }
    public abstract SeverityLevel SeverityLevel { get; }
    public abstract string Reason { get; }
    public abstract HttpStatusCode StatusCode { get; }
}