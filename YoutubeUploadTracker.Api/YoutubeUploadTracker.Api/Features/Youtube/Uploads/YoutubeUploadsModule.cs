using YoutubeUploadTracker.Api.Features.Youtube.Uploads.Service.BackgroundJobs;
using YoutubeUploadTracker.Api.Infrastructure.Hangfire;

namespace YoutubeUploadTracker.Api.Features.Youtube.Uploads;

internal static class YoutubeUploadsModule
{
    public static IServiceCollection AddYoutubeUploadsModule(this IServiceCollection services)
    {
        services.AddSingleton<IHangfireRegistrar, YoutubeUploadImporter>();

        return services;
    }
}