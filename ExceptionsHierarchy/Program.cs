using ExceptionsHierarchy.Exceptions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExceptionsHierarchy;

internal static class Program
{
    private static async Task Main()
    {
        var channel = new InMemoryChannel();
        ApplicationLogger applicationLogger = CreateApplicationLogger(channel, "MktInt");
        try
        {
            var someContextInfo = "Hello Context!";

            var exception = new PayPlansValidationException(new ExceptionData("This is a test message", "RatedStateNotSupportedException"));
            exception.Data.Add("CorrelationId", Guid.NewGuid().ToString("N"));
            exception.Data.Add("InterviewId", Guid.NewGuid().ToString("N"));
            exception.Data.Add("SomeContextInfo", someContextInfo);

            throw exception;

            /*
            **** Doing Structured logging manually ****
            logger.LogError(new EventId((int)LogEvent.PayPlanValidationFailed, LogEvent.PayPlanValidationFailed.ToString()), exception,
                "Message: {Message}, Exception Category: {ExceptionCategory}, Reason: {Reason}, Method Name: {MethodName}, CorrelationId: {CorrelationId}, InterviewId: {InterviewId}, SomeContextInfo: {SomeContextInfo}",
                exception.Message,
                exception.ExceptionCategory,
                exception.Reason,
                nameof(Main),
                Guid.NewGuid().ToString("N"),
                Guid.NewGuid().ToString("N"),
                someContextInfo);
            */
        }
        catch (InterviewBaseException e)
        {
            applicationLogger.LogException(e);
        }
        finally
        {
            channel.Flush();
            await Task.Delay(TimeSpan.FromMilliseconds(1000));
            channel.Dispose();
        }
    }

    private static ApplicationLogger CreateApplicationLogger(ITelemetryChannel channel, string roleName)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        IServiceCollection services = new ServiceCollection();
        services
            .Configure<TelemetryConfiguration>(config => config.TelemetryChannel = channel)
            .AddLogging(builder =>
            {
                builder
                    .ClearProviders()
                    .AddApplicationInsights(
                        configureTelemetryConfiguration: (config) => config.ConnectionString = configuration["ApplicationInsights:ConnectionString"],
                        configureApplicationInsightsLoggerOptions: (options) => { }
            )
            .AddJsonConsole(options =>
                {
                    options.JsonWriterOptions = new System.Text.Json.JsonWriterOptions() { Indented = true };
                });
            });

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ILogger<DomainFacade> logger = serviceProvider.GetRequiredService<ILogger<DomainFacade>>();
        return new ApplicationLogger(logger, roleName);
    }
}