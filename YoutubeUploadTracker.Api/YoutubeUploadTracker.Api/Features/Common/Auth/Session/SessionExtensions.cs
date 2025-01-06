using YoutubeUploadTracker.Api.Features.Common.Auth.Session.Models;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Session;

internal static class SessionExtensions
{
    public static IServiceCollection RegisterSession(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserContext>();
        return serviceCollection;
    }
}