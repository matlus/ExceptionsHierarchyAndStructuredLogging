namespace ExceptionsHierarchy;

public record ExceptionData(string Message, string MessageId, Severity Severity = Severity.Error);

public sealed record ExceptionData<TContextInfo> : ExceptionData
{
    public ExceptionData(string Message, string MessageId, TContextInfo ContextInfo)
        : base(Message, MessageId, Severity.Error)
    {
        this.ContextInfo = ContextInfo;
    }

    public required TContextInfo ContextInfo { get; init; }
}