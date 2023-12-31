﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsHierarchy.Exceptions;

public sealed class PayPlansValidationException : InterviewBusinessBaseException
{
    public PayPlansValidationException(ExceptionData exceptionData, LogEvent logEvent = LogEvent.OnPayPlansValidation) : base(exceptionData, logEvent) { }
    public PayPlansValidationException(ExceptionData exceptionData, LogEvent logEvent, Exception inner) : base(exceptionData, logEvent, inner) { }

    public override string Reason => "Pay Plans Data Invalid";
}