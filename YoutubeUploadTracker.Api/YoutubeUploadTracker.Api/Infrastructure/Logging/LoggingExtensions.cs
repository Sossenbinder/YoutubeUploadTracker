using Serilog;
using Serilog.Core;

namespace YoutubeUploadTracker.Api.Infrastructure.Logging;

internal static class LoggingExtensions
{
    public static ILoggingBuilder AddCustomSerilog(this ILoggingBuilder loggingBuilder, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        var serilogLogger = BuildSerilogLogger(configuration, hostEnvironment);

        return loggingBuilder
            .ClearProviders()
            .AddSerilog(serilogLogger);
    }

    private static Logger BuildSerilogLogger(IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich
            .With(new IfNotPresentEnricher("ServiceName", "YoutubeUploadTracker"))
            .Enrich
            .With(new IfNotPresentEnricher("Environment", hostEnvironment.EnvironmentName))
            .CreateLogger();
    }
}