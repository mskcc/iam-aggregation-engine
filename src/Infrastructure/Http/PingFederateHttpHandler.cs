using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Http;

/// <summary>
/// Represents a handler responsible for authenticating HTTP requests against the Ping Federate API.
/// </summary>
public class PingFederateHttpHandler : DelegatingHandler
{
    private readonly ILogger<PingFederateHttpHandler> _logger;
    private readonly PingFederateOptions _pingFederateOptions;

    /// <summary>
    /// Creates an instance of <see cref="PingFederateHttpHandler"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pingFederateOptions"></param>
    public PingFederateHttpHandler(ILogger<PingFederateHttpHandler> logger, 
        IOptionsMonitor<PingFederateOptions> pingFederateOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingFederateOptions);
        
        _logger = logger;
        _pingFederateOptions = pingFederateOptions.CurrentValue;
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

    private async Task<HttpResponseMessage> PerformRequest(HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Setting the 'X-XSRF-Header' header before adding authorization headers");
        request.Headers.Add("X-XSRF-Header", "PingFederate");
        
        _logger.LogDebug("Setting the 'Authorization' header before performing requests");
        var credentials = $"{_pingFederateOptions.Username}:{_pingFederateOptions.Password}";
        var bytes = Encoding.UTF8.GetBytes(credentials);
        var base64EncodedCredentials = Convert.ToBase64String(bytes);
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedCredentials);
        
        _logger.LogDebug("Performing request");
        var response = await base.SendAsync(request, cancellationToken);
        
        _logger.LogDebug("Completed performing the request");
        return response;
    }
}