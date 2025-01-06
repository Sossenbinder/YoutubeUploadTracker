using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Config;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Api;

internal static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints, IServiceProvider serviceProvider)
    {
        endpoints.MapGet("/login", () =>
        {
            var googleAuthOptions = serviceProvider.GetRequiredService<IOptions<GoogleAuthOptions>>().Value;
            
            var properties = new AuthenticationProperties
            {
                RedirectUri = googleAuthOptions.RedirectUri,
            };

            return Results.Challenge(properties, [GoogleDefaults.AuthenticationScheme]);
        });

        endpoints.MapPost("/logout", async httpContext =>
        {
            await httpContext.SignOutAsync();
        }).RequireAuthorization();
        
        

        return endpoints;
    }
}