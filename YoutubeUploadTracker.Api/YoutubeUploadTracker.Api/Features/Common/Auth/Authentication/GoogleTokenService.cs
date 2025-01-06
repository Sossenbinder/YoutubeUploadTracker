using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using YoutubeUploadTracker.Api.Features.Common.Auth.Authentication.Config;

namespace YoutubeUploadTracker.Api.Features.Common.Auth.Authentication;

file record RefreshedAccessTokenSet([property: JsonPropertyName("access_token")] string? NewAccessToken, [property: JsonPropertyName("expires_in")] int? Expiration);

internal sealed class GoogleTokenService(
    HttpClient httpClient,
    IOptions<GoogleAuthOptions> googleAuthOptions)
{
    public async Task<(string? NewAccessToken, DateTime? Expiration)> RefreshAccessTokenAsync(string refreshToken)
    {
        var googleAuthConfig = googleAuthOptions.Value;
        
        var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", googleAuthConfig.ClientId },
            { "client_secret", googleAuthConfig.ClientSecret },
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken },
        }));

        response.EnsureSuccessStatusCode();

        var parsedResponse = (await response.Content.ReadFromJsonAsync<RefreshedAccessTokenSet>())!;
        // Subtract 60 seconds to be safe
        return new(parsedResponse.NewAccessToken, DateTime.UtcNow.AddSeconds(parsedResponse.Expiration!.Value - 60));
    }
}