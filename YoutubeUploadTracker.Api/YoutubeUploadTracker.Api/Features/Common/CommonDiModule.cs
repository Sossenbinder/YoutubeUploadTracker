using Microsoft.EntityFrameworkCore;
using YoutubeUploadTracker.Api.Features.Common.Config;
using YoutubeUploadTracker.Api.Features.Common.Persistence;

namespace YoutubeUploadTracker.Api.Features.Common;

internal static class CommonDiModule
{
    public static IServiceCollection RegisterCommonFunctionality(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<YoutubeUploadTrackerDbContext>(options =>
        {
            var settings = configuration.GetSection(ConfigSections.Postgresql).Get<PostgresqlSettings>();
            options.UseNpgsql(settings!.ConnectionString);
        });
        
        
        return serviceCollection;
    }
}