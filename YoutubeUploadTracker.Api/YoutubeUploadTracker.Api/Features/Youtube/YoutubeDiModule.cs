using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using YoutubeUploadTracker.Api.Features.Youtube.Config;
using YoutubeUploadTracker.Api.Features.Youtube.Service;

namespace YoutubeUploadTracker.Api.Features.Youtube;

internal static class YoutubeDiModule
{
    public static IServiceCollection RegisterYoutube(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<YoutubeApiSettings>(configuration.GetSection(ConfigSections.Youtube));

        serviceCollection.AddSingleton<YouTubeService>(sp =>
        {
            var clientInitializer = new BaseClientService.Initializer()
            {
                ApiKey = sp.GetRequiredService<IOptions<YoutubeApiSettings>>().Value.ApiKey
            };
            return new YouTubeService(clientInitializer);
        });

        serviceCollection.AddScoped<YoutubeChannelRegistrationService>();

        return serviceCollection;
    }
}