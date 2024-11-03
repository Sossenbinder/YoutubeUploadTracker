using Serilog.Core;
using Serilog.Events;

namespace YoutubeUploadTracker.Api.Infrastructure.Logging;

internal sealed class IfNotPresentEnricher(string name, string value) : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(name, value));
    }
}