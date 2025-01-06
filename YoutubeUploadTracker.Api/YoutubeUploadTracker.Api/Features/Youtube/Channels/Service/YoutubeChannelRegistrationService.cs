using Google.Apis.YouTube.v3;
using YoutubeUploadTracker.Api.Features.Common.Persistence;

namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Service;

public sealed class YoutubeChannelRegistrationService(
    YouTubeService youTubeService,
    YoutubeUploadTrackerDbContext dbContext)
{
    public async Task FindAndRegisterChannel(string identifier)
    {
        var searchRequest = youTubeService.Search.List("snippet");
        searchRequest.Q = identifier;
        searchRequest.Type = "channel";
        searchRequest.MaxResults = 1;
        var searchResponse = await searchRequest.ExecuteAsync();

        if (searchResponse.Items.Count == 0)
        {
            return;
        }

        var request = youTubeService.Channels.List("contentDetails");
        request.Id = searchResponse.Items[0].Snippet.ChannelId;

        var channelResponse = await request.ExecuteAsync();
    }
}