using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ServiceNow
{
    /// <summary>
    /// Represents a service for interfacing with the Service Now API.
    /// </summary>
    public interface IServiceNowService
    {
        /// <summary>
        /// Gets the Service Now applications from the configured Service Now SQL table instance.
        /// </summary>
        public Task<IEnumerable<ServiceNowApplication>> GetServiceNowApplicationsAsync(PaginationFilter paginationFilter);

        /// <summary>
        /// Gets the Service Now applications from the configured Service Now SQL table instance.
        /// </summary>
        public Task<IEnumerable<ServiceNowApplication>> GetServiceNowApplicationsAsync();

        /// <summary>
        /// Gets the count of Service Now applications from the configured Service Now SQL table instance.
        /// </summary>
        /// <returns></returns>
        public int GetServiceNowApplicationsCount();

        /// <summary>
        /// Aggregates Service Now applications from the configured Service Now SQL table instance.
        /// </summary>
        public Task<IEnumerable<ServiceNowApplication>> AggregateServiceNowApplicationsAsync();

        /// <summary>
        /// Purges Service Now applications from database.
        /// </summary>
        /// <returns><see cref="Void"/></returns>
        public Task PurgeServiceNowApplicationsAsync();

        /// <summary>
        /// Gets the service now users asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ServiceNowUser>> GetServiceNowUsersAsync();

        /// <summary>
        /// Gets the service now users asynchronously with pagination options.
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public Task<IEnumerable<ServiceNowUser>> GetServiceNowUsersAsync(PaginationFilter paginationFilter);

        /// <summary>
        /// Gets the service now users count asynchronously.
        /// </summary>
        /// <returns></returns>
        public int GetServiceNowUsersCount();

        /// <summary>
        /// Aggregates the service now users asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ServiceNowUser>> AggregateServiceNowUsersAsync();

        /// <summary>
        /// Purges Service Now users from database.
        /// </summary>
        /// <returns><see cref="Void"/></returns>
        public Task PurgeServiceNowUsersAsync();
    }
}