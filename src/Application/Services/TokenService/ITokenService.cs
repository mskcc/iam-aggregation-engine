namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.TokenService;

public interface ITokenService
{
    /// <summary>
    /// Authenticate against the Ping One API.
    /// </summary>
    Task<string> AuthenticateAsync();

    /// <summary>
    /// Get the access token.
    /// </summary>
    string AccessToken { get; }
}