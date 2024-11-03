using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using YoutubeUploadTracker.Api.Features.Youtube.Api.Models;
using YoutubeUploadTracker.Api.Features.Youtube.Service;

namespace YoutubeUploadTracker.Api.Features.Youtube.Api;

internal static class YoutubeChannelEndpoints
{
    public static IEndpointRouteBuilder MapYoutubeChannelEndpoints(this IEndpointRouteBuilder builder, ApiVersionSet apiVersionSet)
    {
        builder.MapGroup("v{version:apiVersion}/channels/tracked")
            .WithApiVersionSet(apiVersionSet)
            .MapEndpoints();
        
        return builder;
    }

    private static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapPost("/register", async ([FromQuery] string identifier, [FromServices] YoutubeChannelRegistrationService youtubeChannelRegistrationService) =>
            {
                await youtubeChannelRegistrationService.FindAndRegisterChannel(identifier);
                return Results.Ok();
            });
        
        builder.MapGet("/", async ([FromServices] YoutubeChannelService youtubeChannelService, [FromQuery] string? name = null) =>
        {
            var channels = await youtubeChannelService.FindChannels();
            return Results.Ok(channels.Select(x => new YoutubeChannelApiModel(x)));
        });
    }
}