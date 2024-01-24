namespace ExceptionsHierarchy.Exceptions;

public sealed class PayPlansValidationException : InterviewBusinessBaseException
{
    public PayPlansValidationException(string message, string messageId, LogEvent logEvent = LogEvent.OnPayPlansValidation) : base(message, messageId, logEvent) { }
    public PayPlansValidationException(string message, string messageId, LogEvent logEvent, Exception inner) : base(message, messageId, logEvent, inner) { }

    public override string Reason => "Pay Plans Data Invalid";
}