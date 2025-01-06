using YoutubeUploadTracker.Api.Features.Common.Auth.Session.Models;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Models;

internal sealed class UserInfo(UserContext userContext)
{
    public string Id { get; init; } = userContext.UserId;
    
    public string Name { get; init; } = userContext.Name;

    public string? Email { get; init; } = userContext.Email;

    public string? AvatarUrl { get; init; } = userContext.AvatarUrl;
}