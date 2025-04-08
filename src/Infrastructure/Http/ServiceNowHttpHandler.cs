using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Http;

/// <summary>
/// Represents a handler responsible for authenticating HTTP requests against the Service Now API.
/// </summary>
public class ServiceNowHttpHandler : DelegatingHandler
{
    private readonly ILogger<ServiceNowHttpHandler> _logger;
    private readonly ServiceNowOptions _serviceNowOptions;

    /// <summary>
    /// Creates an instance of <see cref="ServiceNowHttpHandler"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceNowOptions"></param>
    public ServiceNowHttpHandler(ILogger<ServiceNowHttpHandler> logger, 
        IOptionsMonitor<ServiceNowOptions> serviceNowOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(serviceNowOptions);
        
        _logger = logger;
        _serviceNowOptions = serviceNowOptions.CurrentValue;
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
        _logger.LogDebug("Setting the 'Authorization' header before performing requests");
        var credentials = $"{_serviceNowOptions.Username}:{_serviceNowOptions.Password}";
        var bytes = Encoding.UTF8.GetBytes(credentials);
        var base64EncodedCredentials = Convert.ToBase64String(bytes);
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedCredentials);
        
        _logger.LogDebug("Performing request");
        var response = await base.SendAsync(request, cancellationToken);
        
        _logger.LogDebug("Completed performing the request");
        return response;
    }
}