using Hangfire;
using YoutubeUploadTracker.Api.Infrastructure.Hangfire;

namespace YoutubeUploadTracker.Api.Features.Youtube.Uploads.Service.BackgroundJobs;

internal sealed class YoutubeUploadImporter : IHangfireRegistrar
{
    public void RegisterRecurringJobs(IRecurringJobManager recurringJobManager)
    {
    }
}