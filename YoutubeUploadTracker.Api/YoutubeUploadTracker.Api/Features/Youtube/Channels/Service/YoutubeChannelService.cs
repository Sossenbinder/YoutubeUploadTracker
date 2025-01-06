using Microsoft.EntityFrameworkCore;
using YoutubeUploadTracker.Api.Features.Common.Persistence;
using YoutubeUploadTracker.Api.Features.Youtube.Channels.Model;

namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Service;

internal sealed class YoutubeChannelService(YoutubeUploadTrackerDbContext dbContext)
{
    public async Task<List<YoutubeChannel>> FindChannels(string? pageCursor = null, string? userId = null)
    {
        var channels = dbContext
            .YoutubeChannels
            .AsQueryable();

        if (userId is not null)
        {
            channels = channels.Where(channel => channel.UserSubscriptions
                .Any(subscription => subscription.UserId == userId));
        }

        if (!string.IsNullOrEmpty(pageCursor))
        {
            channels = channels.Where(x => string.Compare(x.ChannelId, pageCursor) > 0);
        }

        channels = channels
            .OrderBy(x => x.ChannelId)
            .Take(50);

        var retrievedChannels = await channels.ToListAsync();

        return retrievedChannels
            .Select(x => x.ToModel())
            .ToList();
    }
}