# Exceptions Hierarchy and Structured Logging
Having a well thought out Exceptions Hierarchy goes a long way towards proper Exception Handling as well as logging. The diagram below depicts the proposed Hierarchy

![alt text](https://github.com/matlus/ExceptionsHierarchyAndStructuredLogging/blob/master/ExceptionsHierarchy.png?raw=true "Exceptions Hierarchy")

Fig: 1 - Showing the recommended Hierarchy.
In Fig: 1 above, exceptions marked with a circle are abstract. Those marked with a red star are public and sealed and the exception you would throw. All exceptions you throw should descent from those marked as abstract. 
Custom exceptions should be highly specific rather than generalized. Their names should indicate a very particular issue and thus are generally thrown from a single place in your application and for a very specific problem. It’s possible that the same exception type is thrown from more than one place in your code but the reason should be exactly the same.
Carefully chose the correct ancestor for your custom exceptions.
## Technical Exceptions
Technical exceptions are exceptions that you need to attend to, and thus you’d typically have alerts set up for, to be notified. Typically, these are issues that won’t fix themselves and thus need your attention right away, but they are also technical in nature such as:
1.	Not able to reach a service (database, downstream service, platform service etc.)
2.	Configuration settings missing or invalid
3.	All .NET exceptions (Null Reference, Index out of bounds, sequence does not contain any elements etc.)

## Business Exceptions
Business Exceptions are further broken-down Business Critical exceptions. So essentially you have two types of Business Exceptions
1.	Business Exceptions
2.	Business Critical Exceptions

Generally, business exceptions are those that violate business requirements (input arguments) and business rules. However, there are situations where certain inputs should not be sent to your system or should not be violated and thus need your attention right away. These exceptions should descend from Business-Critical exceptions. Validation exception and business rule violations should decent from Business Exception.

## Structured Logging
The code provided make it really simple to add additional context information to exception that automatically shows up in the exception messages as well as in the logs as structured logging. To log exceptions, simply pass the exception caught as a parameter to the `LogException` method.

## Log Severity
Technical exceptions are logged as “Critical” in the logs
Business Critical exceptions are logged as “Critical” in the logs
Business exceptions are logged as “Error” in the logs

This gives you the ability to change the log level in the appsettings.json file to turn on/off Business exceptions while continuing to log Technical and Business Critical logs.
