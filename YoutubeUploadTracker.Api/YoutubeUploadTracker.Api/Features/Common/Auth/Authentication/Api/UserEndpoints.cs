using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Models;
using YoutubeUploadTracker.Api.Features.Common.Auth.Session.Models;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Api;

internal static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints, IServiceProvider serviceProvider)
    {
        endpoints.MapGet("/user", (HttpContext httpContext, UserContext userContext) =>
        {
            if (httpContext.User.Identity is not { IsAuthenticated: true })
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new UserInfo(userContext));
        });

        return endpoints;
    }
}