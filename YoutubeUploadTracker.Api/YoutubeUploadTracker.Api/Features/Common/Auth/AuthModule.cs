using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Api;
using YoutubeUploadTracker.Api.Features.Common.Auth.Session;

namespace YoutubeUploadTracker.Api.Features.Common.Auth;

internal static class AuthModule
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder endpoints, IServiceProvider serviceProvider)
    {
        return endpoints
            .MapAuthEndpoints(serviceProvider)
            .MapUserEndpoints(serviceProvider);
    }
    
    public static IServiceCollection RegisterCustomAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.RegisterAuthentication(configuration);
        serviceCollection.RegisterSession();
        return serviceCollection;
    }
}