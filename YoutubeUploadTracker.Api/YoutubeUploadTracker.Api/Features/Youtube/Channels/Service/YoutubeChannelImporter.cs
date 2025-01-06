using System.Runtime.CompilerServices;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.EntityFrameworkCore;
using YoutubeUploadTracker.Api.Features.Common.Auth.Session.Models;
using YoutubeUploadTracker.Api.Features.Common.Persistence;
using YoutubeUploadTracker.Api.Features.Common.Persistence.Entity;

namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Service;

internal sealed class YoutubeChannelImporter(
    YouTubeService youTubeService,
    YoutubeUploadTrackerDbContext dbContext)
{
    public async Task ImportChannels(UserContext userContext, CancellationToken cancellationToken)
    {
        await foreach (var subscriptionsChunk in RetrieveSubscriptionsForUser(userContext, cancellationToken))
        {
            await ProcessSubscriptionChunk(userContext, subscriptionsChunk, cancellationToken);
        }
    }

    private async Task ProcessSubscriptionChunk(UserContext userContext, IList<Subscription> subscriptions, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var channelIds = subscriptions.Select(x => x.Snippet?.ResourceId?.ChannelId).ToHashSet();

        var existingChannels = await dbContext.YoutubeChannels
            .Where(x => channelIds.Contains(x.ChannelId))
            .ToListAsync(cancellationToken);

        var existingIds = existingChannels.Select(x => x.ChannelId).ToHashSet();

        List<YoutubeChannelEntity> allChannels = [..existingChannels];

        foreach (var subscription in subscriptions)
        {
            var snippet = subscription.Snippet!;
            var channelId = snippet.ResourceId?.ChannelId!;
            if (existingIds.Contains(channelId))
            {
                continue;
            }

            var channel = new YoutubeChannelEntity
            {
                ChannelId = channelId,
                Name = snippet.Title,
                Thumbnail88 = snippet.Thumbnails.Default__.Url,
                Thumbnail240 = snippet.Thumbnails.Medium.Url,
                Thumbnail800 = snippet.Thumbnails.High.Url
            };

            dbContext.YoutubeChannels.Add(channel);
            allChannels.Add(channel);
        }

        dbContext.YoutubeSubscriptionEntities.AddRange(allChannels.Select(x => new UserYoutubeSubscriptionEntity
        {
            UserId = userId,
            YoutubeChannel = x
        }));

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async IAsyncEnumerable<IList<Subscription>> RetrieveSubscriptionsForUser(UserContext userContext, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var accessToken = await userContext.GetAccessToken();

        var subscriptionListRequest = youTubeService.Subscriptions.List("snippet");
        subscriptionListRequest.Mine = true;
        subscriptionListRequest.Credential = GoogleCredential.FromAccessToken(accessToken);
        subscriptionListRequest.MaxResults = 100;

        var subscriptionListResponse = await subscriptionListRequest.ExecuteAsync(cancellationToken);
        yield return subscriptionListResponse.Items;

        while (subscriptionListResponse.NextPageToken is not null)
        {
            subscriptionListRequest.PageToken = subscriptionListResponse.NextPageToken;
            subscriptionListResponse = await subscriptionListRequest.ExecuteAsync(cancellationToken);
            yield return subscriptionListResponse.Items;
        }
    }
}