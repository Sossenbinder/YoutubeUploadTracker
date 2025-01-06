using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoutubeUploadTracker.Api.Features.Youtube.Channels.Model;

namespace YoutubeUploadTracker.Api.Features.Common.Persistence.Entity;

public sealed class YoutubeChannelEntity
{
    public int Id { get; init; }

    public required string ChannelId { get; init; }

    public required string Name { get; init; }

    public required string Thumbnail88 { get; init; }

    public required string Thumbnail240 { get; init; }

    public required string Thumbnail800 { get; init; }

    public List<UserYoutubeSubscriptionEntity> UserSubscriptions { get; init; } = [];

    public YoutubeChannel ToModel() => new()
    {
        ChannelId = ChannelId,
        Name = Name,
        Thumbnail88 = Thumbnail88,
        Thumbnail240 = Thumbnail240,
        Thumbnail800 = Thumbnail800
    };

    public class YoutubeChannelEntityConfiguration : IEntityTypeConfiguration<YoutubeChannelEntity>
    {
        public void Configure(EntityTypeBuilder<YoutubeChannelEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ChannelId).HasMaxLength(64);
            builder.Property(x => x.Name).HasMaxLength(512);
            builder.Property(x => x.Thumbnail88).HasMaxLength(2048);
            builder.Property(x => x.Thumbnail240).HasMaxLength(2048);
            builder.Property(x => x.Thumbnail800).HasMaxLength(2048);
        }
    }
}