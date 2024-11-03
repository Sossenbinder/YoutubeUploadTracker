using Microsoft.EntityFrameworkCore;
using YoutubeUploadTracker.Api.Features.Common.Persistence;

namespace YoutubeUploadTracker.Api.Infrastructure.BackgroundServices;

internal sealed class EfMigrationService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        await using var ctx = scope.ServiceProvider.GetRequiredService<YoutubeUploadTrackerDbContext>();
        
        await ctx.Database.MigrateAsync(cancellationToken: stoppingToken);
    }
}