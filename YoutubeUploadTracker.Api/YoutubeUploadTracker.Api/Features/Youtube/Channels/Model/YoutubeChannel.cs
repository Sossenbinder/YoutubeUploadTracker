namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Model;

public sealed class YoutubeChannel
{
    public required string ChannelId { get; init; }

    public required string Name { get; init; }

    public required string Thumbnail88 { get; init; }

    public required string Thumbnail240 { get; init; }

    public required string Thumbnail800 { get; init; }

    public string ChannelLink => $"https://www.youtube.com/channel/{ChannelId}";
}