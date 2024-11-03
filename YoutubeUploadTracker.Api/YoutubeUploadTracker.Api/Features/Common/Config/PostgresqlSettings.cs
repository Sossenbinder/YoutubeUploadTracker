namespace YoutubeUploadTracker.Api.Features.Common.Config;

internal sealed class PostgresqlSettings
{
    public required string ConnectionString { get; init; }
}