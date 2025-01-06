using Microsoft.EntityFrameworkCore;
using YoutubeUploadTracker.Api.Features.Common.Persistence;

namespace YoutubeUploadTracker.Api.Infrastructure.BackgroundServices;

internal sealed class EfMigrationService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        await using var ctx = scope.ServiceProvider.GetRequiredService<YoutubeUploadTrackerDbContext>();
        
        await ctx.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}