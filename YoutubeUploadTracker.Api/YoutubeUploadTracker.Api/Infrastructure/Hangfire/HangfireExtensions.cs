using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Options;

namespace YoutubeUploadTracker.Api.Infrastructure.Hangfire;

internal static class HangfireExtensions
{
    public static IServiceCollection AddCloudHangFire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.UseRecommendedSerializerSettings();
            config.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(configuration["Postgresql:ConnectionString"]), new()
            {
                UseSlidingInvisibilityTimeout = true
            });
        });

        services.AddHangfireServer();
        services.Configure<HangfireOptions>(configuration.GetSection(HangfireOptions.SectionName));

        services.AddHostedService<BackgroundJobsManager>();

        return services;
    }

    public static void AddHangfireDashboard(this WebApplication app)
    {
        var hangfireOptions = app.Services
            .GetRequiredService<IOptions<HangfireOptions>>()
            .Value;

        app.UseHangfireDashboard("/jobs", new()
        {
            DashboardTitle = "LEAGUES Cloud",
            Authorization = app.Environment.IsDevelopment()
                ? []
                :
                [
                    new BasicAuthAuthorizationFilter(new()
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users =
                        [
                            new()
                            {
                                Login = "admin",
                                PasswordClear = hangfireOptions!.DashboardKeys[0]
                            }
                        ]
                    })
                ]
        });
    }

    private class HangfireOptions
    {
        public required List<string> DashboardKeys { get; init; } = [];

        public const string SectionName = "Hangfire";
    }
}