using YoutubeUploadTracker.Api.Features.Youtube.Model;

namespace YoutubeUploadTracker.Api.Features.Youtube.Api.Models;

internal sealed class YoutubeChannelApiModel(YoutubeChannel channel)
{
    public string ChannelId { get; init; } = channel.ChannelId;

    public string Name { get; init; } = channel.Name;
}