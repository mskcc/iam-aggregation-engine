using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="LegacyConnection"/>
/// </summary>
public static class LegacyConnectionExtensions
{
    /// <summary>
    /// Maps the conenction passed in to a DTO without overriding primary keys.
    /// </summary>
    /// <param name="legacyConnection">Existing <see cref="LegacyConnection"/> to map to.</param>
    public static LegacyConnection MapFrom(this LegacyConnection existingDto, LegacyConnection legacyConnection)
    {
        ArgumentNullException.ThrowIfNull(legacyConnection);
        ArgumentNullException.ThrowIfNull(existingDto);

        existingDto.Id = legacyConnection.Id;
        existingDto.ACSEndpoint = legacyConnection.ACSEndpoint;
        existingDto.APMNumber = legacyConnection.APMNumber;
        existingDto.ApplicationName = legacyConnection.ApplicationName;
        existingDto.BaseUrl = legacyConnection.BaseUrl;
        existingDto.BusinessOwner = legacyConnection.BusinessOwner;
        existingDto.ConnectionName = legacyConnection.ConnectionName;
        existingDto.EntityID = legacyConnection.EntityID;
        existingDto.Environment = legacyConnection.Environment;
        existingDto.Instance = legacyConnection.Instance;
        existingDto.IsActive = legacyConnection.IsActive;
        existingDto.CreationDate = legacyConnection.CreationDate;
        existingDto.ModificationDate = legacyConnection.ModificationDate;
        existingDto.ConditionalIssuanceCriteria = legacyConnection.ConditionalIssuanceCriteria;
        existingDto.ExpressionIssuanceCriteria = legacyConnection.ExpressionIssuanceCriteria;
        existingDto.PingConnectionID = legacyConnection.PingConnectionID;
        existingDto.TechnicalOwner = legacyConnection.TechnicalOwner;
        existingDto.TicketNumber = legacyConnection.TicketNumber;
        existingDto.Type = legacyConnection.Type;

        return existingDto;
    }

    /// <summary>
    /// Syncs up the database with which connections were removed from the configured Ping Federate instance
    /// </summary>
    /// <param name="persistedConnections"></param>
    /// <param name="updatedConnections"></param>
    /// <returns></returns>
    public static List<LegacyConnection>? SyncRemovedConnections(this List<LegacyConnection>? persistedConnections, List<LegacyConnection>? updatedConnections)
    {
        ArgumentNullException.ThrowIfNull(persistedConnections);
        ArgumentNullException.ThrowIfNull(updatedConnections);

        persistedConnections.RemoveAll(connectionSet => 
            !updatedConnections.Any(c => c.Id == connectionSet.Id));

        return persistedConnections;
    }
}