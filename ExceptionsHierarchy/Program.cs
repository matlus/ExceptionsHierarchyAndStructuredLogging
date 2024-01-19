using ExceptionsHierarchy.Exceptions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExceptionsHierarchy;

internal static class Program
{
    private static async Task Main()
    {
        using var telemetryChannel = new InMemoryChannel();
        var (host, applicationLogger) = CreateHost(telemetryChannel, "MktInt");
        await ExecuteApplication(telemetryChannel, applicationLogger);

        await Console.Out.WriteLineAsync("Finished Executing Application");
        await Console.Out.WriteLineAsync("Press CTRL + C to shut down...");
        await host.RunAsync();
    }

    private static async Task ExecuteApplication(InMemoryChannel telemetryChannel, ApplicationLogger applicationLogger)
    {
        var correlationId = Guid.NewGuid().ToString("N");

        try
        {
            //// uncomment below line to Demo .NET Exceptions being logged using Structured logging with a Servity of Critical
            ////throw new ArgumentNullException(nameof(telemetryChannel));
            var someContextInfo = $"Some super important Contextual information - {DateTimeOffset.UtcNow.ToLocalTime():o}";

            var exception = new PayPlansValidationException(new ExceptionData("This is a test message", "RatedStateNotSupportedException"));
            exception.Data.Add("CorrelationId", correlationId);
            exception.Data.Add("InterviewId", Guid.NewGuid().ToString("N"));
            exception.Data.Add("SomeContextInfo", someContextInfo);

            throw exception;
            
            ////**** Doing Structured logging manually ****
            ////logger.LogError(new EventId((int)LogEvent.PayPlanValidationFailed, LogEvent.PayPlanValidationFailed.ToString()), exception,
            ////    "Message: {Message}, Exception Category: {ExceptionCategory}, Reason: {Reason}, Method Name: {MethodName}, CorrelationId: {CorrelationId}, InterviewId: {InterviewId}, SomeContextInfo: {SomeContextInfo}",
            ////    exception.Message,
            ////    exception.ExceptionCategory,
            ////    exception.Reason,
            ////    nameof(Main),
            ////    Guid.NewGuid().ToString("N"),
            ////    Guid.NewGuid().ToString("N"),
            ////    someContextInfo);            
        }
        catch (InterviewBaseException e)
        {
            applicationLogger.LogException(e);
        }
        catch (Exception e)
        {
            applicationLogger.LogException(e, correlationId);
        }
        finally
        {
            telemetryChannel.Flush();
            await Task.Delay(TimeSpan.FromMilliseconds(1000));
            telemetryChannel.Dispose();
        }
    }

    private static (IHost, ApplicationLogger) CreateHost(InMemoryChannel telemetryChannel, string roleName)
    {
        IConfigurationRoot configurationRoot = default!;
        var hostBuilder = Host.CreateDefaultBuilder();
        var host = hostBuilder
        .ConfigureHostConfiguration(configurationBuilder =>
        {
            configurationRoot = configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        })
        .ConfigureServices(services =>
        {
            services
            .Configure<TelemetryConfiguration>(config => config.TelemetryChannel = telemetryChannel)
            .AddLogging(builder =>
            {
                builder
                .AddApplicationInsights(
                    configureTelemetryConfiguration: config => config.ConnectionString = configurationRoot["ApplicationInsights:ConnectionString"],
                    configureApplicationInsightsLoggerOptions: options => { })
                .AddJsonConsole(options =>
                {
                    options.JsonWriterOptions = new System.Text.Json.JsonWriterOptions() { Indented = true };
                });
            });
        })
        .Build();

        var logger = host.Services.GetRequiredService<ILogger<DomainFacade>>();
        return (host, new ApplicationLogger(logger, roleName));
    }
}