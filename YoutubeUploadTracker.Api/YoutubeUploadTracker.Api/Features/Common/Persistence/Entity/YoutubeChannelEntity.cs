using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YoutubeUploadTracker.Api.Features.Common.Persistence.Entity;

public sealed class YoutubeChannelEntity
{
    public required int Id { get; set; }
    
    public required string ChannelId { get; set; }
    
    public required string Name { get; set; }
    
    public class YoutubeChannelEntityConfiguration : IEntityTypeConfiguration<YoutubeChannelEntity>
    {
        public void Configure(EntityTypeBuilder<YoutubeChannelEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ChannelId).HasMaxLength(64);
            builder.Property(x => x.Name).HasMaxLength(512);
        }
    }
}