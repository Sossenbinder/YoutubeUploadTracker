using YoutubeUploadTracker.Api.Features.Youtube.Channels.Model;

namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Api.Models;

internal sealed class YoutubeChannelApiModel
{
    public string ChannelId { get; init; }

    public string Name { get; init; }

    public string Thumbnail88 { get; init; }

    public string Thumbnail240 { get; init; }

    public string Thumbnail800 { get; init; }

    public string ChannelLink { get; init; }

    public YoutubeChannelApiModel(YoutubeChannel model)
    {
        ChannelId = model.ChannelId;
        Name = model.Name;
        Thumbnail88 = model.Thumbnail88;
        Thumbnail240 = model.Thumbnail240;
        Thumbnail800 = model.Thumbnail800;
        ChannelLink = model.ChannelLink;
    }
}