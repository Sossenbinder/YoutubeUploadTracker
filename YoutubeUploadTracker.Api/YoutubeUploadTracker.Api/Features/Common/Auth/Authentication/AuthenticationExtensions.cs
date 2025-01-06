using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Config;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Models;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Authentication;

internal static class AuthenticationExtensions
{
    public static IServiceCollection RegisterAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var googleAuthenticationSection = configuration.GetSection(GoogleAuthOptions.ConfigSection);

        serviceCollection.Configure<GoogleAuthOptions>(googleAuthenticationSection);

        serviceCollection.AddHttpClient<GoogleTokenService>(x => x.BaseAddress = new("https://oauth2.googleapis.com/token"));
        
        serviceCollection
            .AddAuthentication(x =>
            {
                x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(googleOptions =>
            {
                var adHocGoogleAuthOptions = googleAuthenticationSection.Get<GoogleAuthOptions>()!;
                
                googleOptions.CallbackPath = adHocGoogleAuthOptions.SigninCallback;
                googleOptions.ClientId = adHocGoogleAuthOptions.ClientId;
                googleOptions.ClientSecret = adHocGoogleAuthOptions.ClientSecret;
                googleOptions.SaveTokens = true;
                googleOptions.AccessType = "offline";
                
                googleOptions.Scope.Add("https://www.googleapis.com/auth/youtube.readonly");
                
                googleOptions.Events = new()
                {
                    OnRedirectToAuthorizationEndpoint = context =>
                    {
                        var uri = context.RedirectUri;
                        if (!uri.Contains("prompt="))
                        {
                            uri = QueryHelpers.AddQueryString(uri, "prompt", "consent");
                        }
                        
                        if (context.Request.Path.StartsWithSegments("/api/login"))
                        {
                            context.Response.Redirect(uri);
                            return Task.CompletedTask;
                        }

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    },
                    OnCreatingTicket = context =>
                    {
                        if (context.Identity is not null && context.User.TryGetProperty("picture", out var picture))
                        {
                            context.Identity.AddClaim(new Claim(ClaimNames.ProfilePictureClaim, picture.GetString()!));
                        }
                        return Task.CompletedTask;
                    },
                };
            });

        return serviceCollection;
    }
}