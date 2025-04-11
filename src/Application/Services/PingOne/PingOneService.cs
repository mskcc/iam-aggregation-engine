using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;


namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;

public class PingOneService : IPingOneService
{
    private readonly ILogger<PingOneService> _logger;
    private readonly PingOneOptions _pingOneOptions;
    private readonly HttpClient _pingOneHttpClient;

    /// <summary>
    /// Creates an instance of the <see cref="PingOneService"/> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pingOneOptionsMonitor"></param>
    public PingOneService(
        ILogger<PingOneService> logger,
        IOptionsMonitor<PingOneOptions> pingOneOptionsMonitor,
        IHttpClientFactory httpClientFactory
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneOptionsMonitor);
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        _logger = logger;
        _pingOneOptions = pingOneOptionsMonitor.CurrentValue;
        _pingOneHttpClient = httpClientFactory.CreateClient(HttpClientNames.PingOneClient);
    }

    /// <inheritdoc/>
    public Task<PingOneResponse> CreateLinkedAccountForLdapGateway(string userId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<PingOneResponse> CreateLinkedAccountForMicrosoftEntra(string userId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<PingOneResponse> CreateLinkedAccountForPingFederate(string userId)
    {
        throw new NotImplementedException();
    }
}