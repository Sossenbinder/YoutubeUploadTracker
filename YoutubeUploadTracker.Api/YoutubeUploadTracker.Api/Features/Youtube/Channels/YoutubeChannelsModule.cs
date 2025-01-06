using YoutubeUploadTracker.Api.Features.Youtube.Channels.Service;

namespace YoutubeUploadTracker.Api.Features.Youtube.Channels;

internal static class YoutubeChannelsModule
{
    public static IServiceCollection AddYoutubeChannelsModule(this IServiceCollection services)
    {
        services.AddScoped<YoutubeChannelRegistrationService>();
        services.AddScoped<YoutubeChannelService>();
        services.AddScoped<YoutubeChannelImporter>();

        return services;
    }
}