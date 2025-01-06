using Hangfire;

namespace YoutubeUploadTracker.Api.Infrastructure.Hangfire;

internal interface IHangfireRegistrar
{
    void RegisterRecurringJobs(IRecurringJobManager recurringJobManager);
}