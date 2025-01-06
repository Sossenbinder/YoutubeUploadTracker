using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using YoutubeUploadTracker.Api.Features.Youtube.Channels;
using YoutubeUploadTracker.Api.Features.Youtube.Channels.Api;
using YoutubeUploadTracker.Api.Features.Youtube.Config;
using YoutubeUploadTracker.Api.Features.Youtube.Uploads;

namespace YoutubeUploadTracker.Api.Features.Youtube;

internal static class YoutubeDiModule
{
    public static IEndpointRouteBuilder MapYoutubeEndpoints(this IEndpointRouteBuilder builder) => builder.MapYoutubeChannelEndpoints();

    public static IServiceCollection RegisterYoutube(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<YoutubeApiSettings>(configuration.GetSection(YoutubeApiSettings.ConfigSection));

        serviceCollection.AddSingleton<YouTubeService>(sp =>
        {
            var clientInitializer = new BaseClientService.Initializer
            {
                ApiKey = sp.GetRequiredService<IOptions<YoutubeApiSettings>>().Value.ApiKey
            };
            return new(clientInitializer);
        });

        serviceCollection.AddYoutubeChannelsModule().AddYoutubeUploadsModule();

        return serviceCollection;
    }
}