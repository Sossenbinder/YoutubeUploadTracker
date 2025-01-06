namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Api.Models;

internal sealed record PaginatedYoutubeChannelsResponse(
    IEnumerable<YoutubeChannelApiModel> Channels,
    string? PageCursor);