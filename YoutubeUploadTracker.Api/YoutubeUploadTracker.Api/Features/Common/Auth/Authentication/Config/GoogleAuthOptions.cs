namespace YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Config;

internal sealed class GoogleAuthOptions
{
    public const string ConfigSection = "Authentication:Google";
    
    public required string ClientId { get; init; }
    
    public required string ClientSecret { get; init; }
    
    public required string RedirectUri { get; init; }
    
    public required string SigninCallback { get; init; }
};