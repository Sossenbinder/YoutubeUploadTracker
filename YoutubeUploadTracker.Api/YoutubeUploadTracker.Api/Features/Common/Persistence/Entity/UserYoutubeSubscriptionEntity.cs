using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YoutubeUploadTracker.Api.Features.Common.Persistence.Entity;

public sealed class UserYoutubeSubscriptionEntity
{
    public required string UserId { get; init; }

    public int YoutubeChannelId { get; init; }

    public YoutubeChannelEntity YoutubeChannel { get; init; } = null!;
    
    public class UserYoutubeChannelMappingConfiguration : IEntityTypeConfiguration<UserYoutubeSubscriptionEntity>
    {
        public void Configure(EntityTypeBuilder<UserYoutubeSubscriptionEntity> builder)
        {
            builder.HasKey(x => new { x.UserId, x.YoutubeChannelId });

            builder.Property(x => x.UserId)
                .HasMaxLength(128);
            
            builder.Property(x => x.YoutubeChannelId)
                .HasMaxLength(256);

            builder.HasIndex(x => x.UserId);
            
            builder
                .HasOne(x => x.YoutubeChannel)
                .WithMany(c => c.UserSubscriptions)
                .HasForeignKey(x => x.YoutubeChannelId);
        }
    }
}