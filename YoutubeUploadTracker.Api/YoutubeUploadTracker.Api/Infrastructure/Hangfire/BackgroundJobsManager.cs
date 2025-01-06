using Hangfire;
using Hangfire.Storage;

namespace YoutubeUploadTracker.Api.Infrastructure.Hangfire;

internal class BackgroundJobsManager(
    IRecurringJobManager recurringJobManager,
    IEnumerable<IHangfireRegistrar> registrars) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        CleanPreviousJobs();

        foreach (var registrar in registrars)
        {
            registrar.RegisterRecurringJobs(recurringJobManager);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void CleanPreviousJobs()
    {
        using var ctx = JobStorage.Current.GetConnection();

        foreach (var recurringJob in ctx.GetRecurringJobs())
        {
            recurringJobManager.RemoveIfExists(recurringJob.Id);
        }
    }
}