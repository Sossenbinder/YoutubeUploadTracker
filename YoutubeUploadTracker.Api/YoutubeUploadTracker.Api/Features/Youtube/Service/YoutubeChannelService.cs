using YoutubeUploadTracker.Api.Features.Youtube.Model;

namespace YoutubeUploadTracker.Api.Features.Youtube.Service;

internal sealed class YoutubeChannelService
{
    public async Task<IReadOnlyList<YoutubeChannel>> FindChannels()
    {
        return new List<YoutubeChannel>();
    }
}