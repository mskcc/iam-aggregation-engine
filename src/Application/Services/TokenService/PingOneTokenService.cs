using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.TokenService;

/// <summary>
/// Service for invoking OAuth 2.0 Client Credentials flow to obtain an 
/// access token from Ping One's configured worker application.
/// </summary>
public class PingOneTokenService : ITokenService
{
    private readonly ILogger<PingOneTokenService> _logger;
    private readonly HttpClient _pingOneOauthHttpClient;
    private readonly PingOneOptions _pingOneOptions;
    private readonly ReaderWriterLockSlim _lock;

    private PingOneAuthentication? PingOneAuthentication;

    /// <summary>
    /// Creates an instance of <see cref="PingOneTokenService"/>.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClient"></param>
    /// <param name="pingOneOptions"></param>
    public PingOneTokenService(
        ILogger<PingOneTokenService> logger,
        IHttpClientFactory httpClientFactory,
        IOptionsMonitor<PingOneOptions> pingOneOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(pingOneOptions);

        _logger = logger;
        _pingOneOauthHttpClient = httpClientFactory.CreateClient(HttpClientNames.PingOneOauthClient);
        _pingOneOptions = pingOneOptions.CurrentValue;
        _lock = new ReaderWriterLockSlim();
    }

    /// <inheritdoc/>
    public string AccessToken 
    { 
        get
        {
            _logger.LogDebug("Entering the read lock for access token");
            _lock.EnterReadLock();
            try
            {
                return PingOneAuthentication?.AccessToken!;
            }
            finally
            {
                _logger.LogDebug("Exiting read lock for access token");
                _lock.ExitReadLock();
            }        
        }
    }

    /// <inheritdoc/>
    public async Task<string> AuthenticateAsync()
    {
        _logger.LogInformation("Performing authentication against the Ping One API");

        _logger.LogDebug("Creating the request body for the OAuth 2.0 Client Credentials flow");
        var body = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
        };
        var content = new FormUrlEncodedContent(body);
        
        _logger.LogDebug("Encoding client credentials for authorization header");
        var credentials = $"{_pingOneOptions.ClientId}:{_pingOneOptions.ClientSecret}";
        var bytes = Encoding.UTF8.GetBytes(credentials);
        var base64EncodedCredentials = Convert.ToBase64String(bytes);

        _logger.LogInformation("Setting the 'Authorization' header before performing requests");
        _pingOneOauthHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedCredentials);

        _logger.LogInformation("Sending request to Ping One's OAuth endpoint to obtain an access token");
        var authenticationResponse = await _pingOneOauthHttpClient.PostAsync($"{_pingOneOptions.EnvironmentId}/as/token", content);
        _logger.LogDebug("HTTP {Method} request to {Endpoint}", 
            HttpMethod.Post, $"{_pingOneOptions.EnvironmentId}/as/token");
        authenticationResponse.EnsureSuccessStatusCode();
        _logger.LogDebug("HTTP {Method} request to {Endpoint} with status code: {StatusCode}", 
            HttpMethod.Post, $"{_pingOneOptions.EnvironmentId}/as/token", HttpStatusCode.OK);

        _logger.LogInformation("Deserializing the response from Ping One's OAuth endpoint");
        var pingOneAuthentication = await authenticationResponse.Content.ReadFromJsonAsync<PingOneAuthentication>();

        ArgumentNullException.ThrowIfNull(pingOneAuthentication?.AccessToken);
        PingOneAuthentication = pingOneAuthentication;

        return pingOneAuthentication.AccessToken;
    }
}