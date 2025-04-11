using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

public class IdentityLinkingService : IIdentityLinkingService
{
    private readonly ILogger<IdentityLinkingService> _logger;

    public IdentityLinkingService(
        ILogger<IdentityLinkingService> logger
    )
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }
    
    /// <inheritdoc/>
    public Task<IdentityLinkingResponse> LinkIdentityFromEntraId()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IdentityLinkingResponse> LinkIdentityFromLdapGateway()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IdentityLinkingResponse> LinkIdentityFromPingFederate()
    {
        throw new NotImplementedException();
    }
}