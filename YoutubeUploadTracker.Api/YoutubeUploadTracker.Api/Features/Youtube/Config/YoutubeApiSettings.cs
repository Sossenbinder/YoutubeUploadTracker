namespace YoutubeUploadTracker.Api.Features.Youtube.Config;

internal sealed class YoutubeApiSettings
{
    public required string ApiKey { get; init; }

    public const string ConfigSection = "Youtube";
}