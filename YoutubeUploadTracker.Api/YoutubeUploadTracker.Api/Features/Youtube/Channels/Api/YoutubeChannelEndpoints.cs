using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YoutubeUploadTracker.Api.Features.Common.Auth.Session.Models;
using YoutubeUploadTracker.Api.Features.Youtube.Channels.Api.Models;
using YoutubeUploadTracker.Api.Features.Youtube.Channels.Service;

namespace YoutubeUploadTracker.Api.Features.Youtube.Channels.Api;

internal static class YoutubeChannelEndpoints
{
    public static IEndpointRouteBuilder MapYoutubeChannelEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("/channels")
            .MapEndpoints();

        return builder;
    }

    private static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/import", async Task<Ok> (
            [FromServices] UserContext userContext,
            [FromServices] YoutubeChannelImporter youtubeChannelImporter,
            CancellationToken cancellationToken) =>
        {
            await youtubeChannelImporter.ImportChannels(userContext, cancellationToken);
            return TypedResults.Ok();
        }).RequireAuthorization();

        builder.MapPost("/register", async Task<Ok> ([FromQuery] string identifier, [FromServices] YoutubeChannelRegistrationService youtubeChannelRegistrationService) =>
        {
            await youtubeChannelRegistrationService.FindAndRegisterChannel(identifier);
            return TypedResults.Ok();
        });

        builder.MapGet("/", async Task<Results<Ok<PaginatedYoutubeChannelsResponse>, UnauthorizedHttpResult>> (
            [FromServices] UserContext userContext,
            [FromServices] YoutubeChannelService youtubeChannelService,
            [FromQuery] bool? mine) =>
        {
            if (mine.HasValue && !userContext.IsAuthenticated)
            {
                return TypedResults.Unauthorized();
            }

            var channels = await youtubeChannelService.FindChannels();
            var pageCursor = channels.Last()?.ChannelId ?? null;

            return TypedResults.Ok(new PaginatedYoutubeChannelsResponse(channels.Select(x => new YoutubeChannelApiModel(x)), pageCursor));
        });
    }
}