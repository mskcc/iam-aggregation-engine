using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="ServiceNowUserExtensions"/>
/// </summary>
public static class ServiceNowUserExtensions
{
    /// <summary>
    /// Maps the service now user passed in to a DTO without overriding primary keys.
    /// </summary>
    /// <param name="serviceNowUserExtensions">Existing <see cref="ServiceNowUserExtensions"/> to map to.</param>
    public static ServiceNowUser MapFrom(this ServiceNowUser existingServiceNowUser, 
        ServiceNowUsersJson serviceNowUsersJson)
    {
        ArgumentNullException.ThrowIfNull(serviceNowUsersJson);

        existingServiceNowUser.Name = serviceNowUsersJson.Name;
        existingServiceNowUser.EmployeeId = serviceNowUsersJson.EmployeeNumber;
        existingServiceNowUser.Email = serviceNowUsersJson.Email;
        existingServiceNowUser.SysId = serviceNowUsersJson.SysId;

        return existingServiceNowUser;
    }

    /// <summary>
    /// Syncs up the database with which connections were removed from the configured Ping Federate instance
    /// </summary>
    /// <param name="persistedConnections"></param>
    /// <param name="updatedConnections"></param>
    /// <returns></returns>
    public static List<ServiceNowUser>? SyncRemovedConnections(this List<ServiceNowUser>? serviceNowUsersList, List<ServiceNowUsersJson>? serviceNowUsersJsons)
    {
        ArgumentNullException.ThrowIfNull(serviceNowUsersList);
        ArgumentNullException.ThrowIfNull(serviceNowUsersJsons);

        serviceNowUsersList.RemoveAll(connectionSet => 
            !serviceNowUsersJsons.Any(c => c.EmployeeNumber == connectionSet.EmployeeId));

        return serviceNowUsersList;
    }
}