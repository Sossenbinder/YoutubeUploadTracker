using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Models;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Session.Models;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor,
    GoogleTokenService googleTokenService)
{
    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

    public string Name => User.Identity!.Name!;

    public string UserId => GetClaimValue(ClaimTypes.NameIdentifier)!;

    public string? Email => GetClaimValue(ClaimTypes.Email);

    public string? AvatarUrl => GetClaimValue(ClaimNames.ProfilePictureClaim);

    private ClaimsPrincipal User => httpContextAccessor.HttpContext!.User;

    public async Task<string?> GetAccessToken()
    {
        var httpContext = httpContextAccessor.HttpContext!;

        var authResult = await httpContext.AuthenticateAsync();

        var accessTokenExpiration = DateTime.Parse(authResult.Properties!.Items[".Token.expires_at"]!).ToUniversalTime();
        if (DateTime.UtcNow <= accessTokenExpiration)
        {
            return authResult.Properties?.GetTokenValue("access_token");
        }

        var (newAccessToken, expiration) = await googleTokenService.RefreshAccessTokenAsync(authResult.Properties.GetTokenValue("refresh_token")!);

        var props = authResult.Properties;
        props.UpdateTokenValue("access_token", newAccessToken!);
        props.Items[".Token.expires_at"] = expiration!.Value.ToString("o");

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            authResult.Principal!,
            props);

        return newAccessToken;
    }

    private string? GetClaimValue(string claimType)
    {
        return User.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}