# Exceptions Hierarchy and Structured Logging
Creating a well-structured Exceptions Hierarchy is pivotal for effective Exception Handling and logging within your application. The diagram presented below illustrates the recommended Hierarchy.

![alt text](https://github.com/matlus/ExceptionsHierarchyAndStructuredLogging/blob/master/ExceptionsHierarchy.png?raw=true "Exceptions Hierarchy")

#### Fig: 1 - Showing the recommended Hierarchy.
In Figure 1, exceptions represented with a circle are abstract. It is crucial that all Custom Exceptions you define and throw descend from these abstract exception and are marked as public and sealed. Keep the depth of hierarchy shallow. Custom exceptions should exhibit high specificity, addressing very particular issues and being thrown from specific locations within your application. While the same exception type may be thrown from multiple places in your code (this is rare), the reasons behind it should remain identical. It is imperative to meticulously choose the correct ancestor for your custom exceptions.

## Technical Exceptions
Technical exceptions demand immediate attention and often necessitate the setup of alerts for notification. These issues are typically of a technical nature and won't resolve themselves. Examples include:

1.Inability to reach a service (e.g., database, downstream service, platform service)
2.	Missing or invalid configuration settings
3.	All .NET exceptions (e.g., Null Reference, Index out of bounds, sequence does not contain any elements)

## Business Exceptions
Business Exceptions can be further categorized into two types:
1.	Business Exceptions
2.	Business Critical Exceptions

### Business Exceptions
These pertain to violations of business requirements and rules. They highlight situations where inputs do not comply with specified business standards. Validation exceptions and business rule violations should descend from Business Exceptions.

### Business Critical Exceptions
These are particularly severe business exceptions that demand immediate attention. Situations where certain inputs should not be sent to the system or should never be violated fall into this category. 

## Structured Logging
The provided code simplifies the process of adding additional context information to exceptions, automatically reflecting in both exception messages and logs through structured logging. To log exceptions, you can utilize the `LogException` method by passing the caught exception as a parameter.

## Log Severity & Logging Strategy
Log severity is differentiated based on the type of exception:
  * Technical exceptions are logged as “Critical” in the logs
  * Business Critical exceptions are also logged as “Critical” in the logs
  * Business exceptions are logged as “Error” in the logs

This logging strategy enables flexibility, allowing you to adjust the log level in the appsettings.json file. This way, you can toggle the logging of Business exceptions on or off while consistently logging Technical and Business Critical logs according to your requirements.
