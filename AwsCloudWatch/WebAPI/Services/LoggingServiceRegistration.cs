using Amazon.CloudWatchLogs;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
using ILogger = Serilog.ILogger;

namespace WebAPI.Services;

public static class LoggingServiceRegistration
{
    public static IServiceCollection AddLoggingServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        ILogger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.AmazonCloudWatch(
                logGroup: $"{builder.Environment.EnvironmentName}/{builder.Environment.ApplicationName}",
                logStreamPrefix: DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                cloudWatchClient: new AmazonCloudWatchLogsClient()
            )
            .CreateLogger();

        builder.Logging.AddSerilog(logger);
        services.AddSingleton(logger);

        return services;
    }
}

