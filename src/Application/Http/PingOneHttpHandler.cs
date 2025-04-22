using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.TokenService;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Http;

/// <summary>
/// Represents a handler responsible for authenticating HTTP requests against the Ping One API.
/// </summary>
public class PingOneHttpHandler : DelegatingHandler
{
    private readonly ILogger<PingOneHttpHandler> _logger;
    private readonly PingOneOptions _pingOneOptions;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Creates an instance of <see cref="PingOneHttpHandler"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="PingOneOptions"></param>
    /// <param name="tokenService"></param>
    public PingOneHttpHandler(
        ILogger<PingOneHttpHandler> logger, 
        IOptionsMonitor<PingOneOptions> pingOneOptions,
        ITokenService tokenService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneOptions);
        ArgumentNullException.ThrowIfNull(tokenService);
        
        _logger = logger;
        _pingOneOptions = pingOneOptions.CurrentValue;
        _tokenService = tokenService;
    }
    
    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        _logger.LogDebug("Sending HTTP request after performing authorization");
        var response = await PerformRequest(request, cancellationToken);
         
        _logger.LogDebug("Completed sending the request with status code '{StatusCode}'", response.StatusCode);
        return response;
    }

    /// <summary>
    /// TODO: This logic should go in the token service and this method should add the JWT auth header to pingone calls. 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<HttpResponseMessage> PerformRequest(HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Setting the 'Authorization' header before performing requests");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);
        
        _logger.LogDebug("Performing request");
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized || response.StatusCode is HttpStatusCode.Forbidden)
        {
            _logger.LogDebug("Received unauthorized response, attempting to refresh the token");
            await _tokenService.AuthenticateAsync();
            
            _logger.LogDebug("Setting the 'Authorization' header before performing requests");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);
            
            _logger.LogDebug("Performing request after refreshing the token");
            response = await base.SendAsync(request, cancellationToken);
        }   
        
        if (response.IsSuccessStatusCode is false)
        {
            _logger.LogError("Request for {Uri} Failed to perform request with status code {StatusCode}",request.RequestUri?.AbsoluteUri.ToString(), response.StatusCode);
        }
        
        _logger.LogDebug("Completed performing the request");
        return response;
    }
}