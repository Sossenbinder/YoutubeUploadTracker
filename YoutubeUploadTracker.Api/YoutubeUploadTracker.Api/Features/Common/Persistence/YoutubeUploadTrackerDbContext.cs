using Microsoft.EntityFrameworkCore;
using YoutubeUploadTracker.Api.Features.Common.Persistence.Entity;

namespace YoutubeUploadTracker.Api.Features.Common.Persistence;

public sealed class YoutubeUploadTrackerDbContext(DbContextOptions<YoutubeUploadTrackerDbContext> options) : DbContext(options)
{
    public required DbSet<YoutubeChannelEntity> Channels { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YoutubeUploadTrackerDbContext).Assembly);
    }
}