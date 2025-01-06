namespace YoutubeUploadTracker.Api.Features.Common.Config;

internal sealed class PostgresqlSettings
{
    public const string ConfigSection = "Postgresql";
    
    public required string ConnectionString { get; init; }
}