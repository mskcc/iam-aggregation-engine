using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

public class IdentityLinkingService : IIdentityLinkingService
{
    private readonly ILogger<IdentityLinkingService> _logger;
    private readonly IPingOneService _pingOneService;
    private readonly DbSet<AzureUsersSource> _azureUsersSource;

    public IdentityLinkingService(
        ILogger<IdentityLinkingService> logger,
        IPingOneService pingOneService,
        ApplicationDbContext applicationDbContext
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneService);
        ArgumentNullException.ThrowIfNull(applicationDbContext);

        _logger = logger;
        _pingOneService = pingOneService;
        _azureUsersSource = applicationDbContext.Set<AzureUsersSource>();
    }
    
    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> LinkIdentityFromEntraId(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);
        
        var objectId = await GetMicrosoftObjectId(samAccountName);
        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForMicrosoftEntra(pingOneUserId, objectId);
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = pingOneAccountLinkingResponse
        };
    }

    /// <inheritdoc/>
    public Task<IdentityLinkingResponse> LinkIdentityFromLdapGateway()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> LinkIdentityFromPingFederate(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);
        
        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForPingFederate(pingOneUserId, samAccountName);
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = pingOneAccountLinkingResponse,
        };
    }

    private async Task<string> GetMicrosoftObjectId(string samAccountName)
    {
        var azureUser = await _azureUsersSource
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.OnPremisesSamAccountName == samAccountName);
        ArgumentNullException.ThrowIfNull(azureUser?.Id);

        return azureUser.Id;
    }
}