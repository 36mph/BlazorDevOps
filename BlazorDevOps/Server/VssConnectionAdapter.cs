using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;

namespace BlazorDevOps.Server;

public class VssConnectionAdapter
{
    private ITokenAcquisition _tokenService;
    private VssConnectionAdapterOptions _options;

    public VssConnectionAdapter(ITokenAcquisition tokenService, VssConnectionAdapterOptions options)
    {
        _tokenService = tokenService;
        _options = options;
    }

    public async Task<VssConnection> GetConnection()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            VssOAuthAccessTokenCredential creds = new VssOAuthAccessTokenCredential(token);
            return new VssConnection(new Uri(_options.BaseUrl), creds);
        }
        else
        {
            throw new UnauthorizedAccessException("Failed to fetch token for Azure DevOps. Please check your settings and try again.");
        }
    }

    private async Task<string> GetToken()
    {
        //try
        //{
            var token = await _tokenService.GetAccessTokenForUserAsync(new[] { _options.Scopes });
            return token;
        //}
        //catch (MicrosoftIdentityWebChallengeUserException ex)
        //{
        //    await _tokenService.ReplyForbiddenWithWwwAuthenticateHeaderAsync(new[] { _options.Scopes }, ex.MsalUiRequiredException);
        //    return string.Empty;
        //}
        //catch (MsalUiRequiredException ex)
        //{
        //    await _tokenService.ReplyForbiddenWithWwwAuthenticateHeaderAsync(new[] { _options.Scopes }, ex, response);
        //    return string.Empty;
        //}
    }
}

public record VssConnectionAdapterOptions(string BaseUrl, string Scopes);
