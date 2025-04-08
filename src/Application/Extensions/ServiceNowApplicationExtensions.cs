using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using System.Reflection;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="ServiceNowApplicationExtensions"/>
/// </summary>
public static class ServiceNowApplicationExtensions
{
    /// <summary>
    /// Maps the service now application passed in to a DTO without overriding primary keys.
    /// </summary>
    /// <param name="serviceNowApplication">Existing <see cref="ServiceNowApplication"/> to map to.</param>
    public static ServiceNowApplication MapFrom(this ServiceNowApplication existingServiceNowApplication, 
        ServiceNowApplicationsJson serviceNowApplication)
    {
        ArgumentNullException.ThrowIfNull(serviceNowApplication);

        var properties = typeof(ServiceNowApplicationsJson).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            var value = property.GetValue(serviceNowApplication);
            var targetProperty = typeof(ServiceNowApplication).GetProperty(property.Name);

            if (value is null || targetProperty is null)
            {
                continue;
            }
            
            if (targetProperty is not null && targetProperty.CanWrite && value!.GetType() != typeof(string))
            {
                var linkValue = (LinkValue) value!;
                targetProperty.SetValue(existingServiceNowApplication, linkValue.Value);
            }

            if (targetProperty is not null && targetProperty.CanWrite && value!.GetType() == typeof(string))
            {
                targetProperty.SetValue(existingServiceNowApplication, value);
            }
        }

        return existingServiceNowApplication;
    }

    /// <summary>
    /// Syncs up the database with which connections were removed from the configured Ping Federate instance
    /// </summary>
    /// <param name="persistedConnections"></param>
    /// <param name="updatedConnections"></param>
    /// <returns></returns>
    public static List<ServiceNowApplication>? SyncRemovedConnections(this List<ServiceNowApplication>? serviceNowApplicationsList, List<ServiceNowApplicationsJson>? serviceNowApplicationJsons)
    {
        ArgumentNullException.ThrowIfNull(serviceNowApplicationsList);
        ArgumentNullException.ThrowIfNull(serviceNowApplicationJsons);

        serviceNowApplicationsList.RemoveAll(connectionSet => 
            !serviceNowApplicationJsons.Any(c => c.Number == connectionSet.Number));

        return serviceNowApplicationsList;
    }
}