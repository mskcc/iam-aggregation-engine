using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;

/// <summary>
/// Service for interfacing with Ping Federate API.
/// </summary>
public interface IPingFederateService
{
    /// <summary>
    /// Gets the SAML connections from the configured Ping Federate instance.
    /// </summary>
    public Task<IEnumerable<SpConnection>> GetSpConnectionsAsync(PaginationFilter paginationFilter);

    /// <summary>
    /// Gets the SAML connections from the configured Ping Federate instance.
    /// </summary>
    public Task<IEnumerable<SpConnection>> GetSpConnectionsAsync();

    /// <summary>
    /// Gets the count of SAML connections from the configured Ping Federate instance.
    /// </summary>
    /// <returns></returns>
    public int GetSpConnectionsCount();

    /// <summary>
    /// Aggregates SAML connections from the configured Ping Federate instance.
    /// </summary>
    public Task<IEnumerable<SpConnection>> AggregateSpConnectionsAsync();

    /// <summary>
    /// Purges SP connections from database.
    /// </summary>
    /// <returns><see cref="Void"/></returns>
    public Task PurgeSpConnectionsAsync();

    /// <summary>
    /// Gets the OIDC connections from the configured Ping Federate instance.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<OidcClient>> GetOidcConnectionsAsync(PaginationFilter paginationFilter);

    /// <summary>
    /// Gets the OIDC connections from the configured Ping Federate instance.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<OidcClient>> GetOidcConnectionsAsync();

    /// <summary>
    /// Gets the count of OIDC connections from the configured Ping Federate instance.
    /// </summary>
    /// <returns></returns>
    public int GetOidcConnectionsCount();

    /// <summary>
    /// Aggregates OIDC connections from the configured Ping Federate instance.
    /// </summary>
    public Task<IEnumerable<OidcClient>> AggregateOidcConnectionsAsync();

    /// <summary>
    /// Purges SP connection from database.
    /// </summary>
    /// <returns><see cref="Void"/></returns>
    public Task PurgeOidcConnectionsAsync();
}