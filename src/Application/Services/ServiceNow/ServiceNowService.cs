using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ServiceNow;

/// <summary>
/// Represents a service for interfacing with the Service Now API.
/// </summary>
public class ServiceNowService : IServiceNowService
{
    private readonly ILogger<ServiceNowService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ServiceNowOptions _serviceNowOptions;
    private readonly IResourceStateService _resourceStateService;
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Creates an instance of <see cref="ServiceNowService"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <param name="serviceNowOptions"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="httpClientFactory"></param>
    public ServiceNowService(ILogger<ServiceNowService> logger, 
        ApplicationDbContext context,
        IOptionsMonitor<ServiceNowOptions> serviceNowOptions,
        IResourceStateService resourceStateService,
        IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(serviceNowOptions);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        _logger = logger;
        _context = context;
        _serviceNowOptions = serviceNowOptions.CurrentValue;
        _resourceStateService = resourceStateService;
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ServiceNowApplication>> AggregateServiceNowApplicationsAsync()
    {
        _logger.LogInformation("Aggregating Service Now applications from the configured Service Now SQL table instance");
        _resourceStateService.IsServiceNowApplicationsAggregationRunning = true;
        _logger.LogDebug("Creating a new HTTP client for interfacing with the Service Now API");
        var serviceNowClient = _httpClientFactory.CreateClient(HttpClientNames.ServiceNowClient);
        var serviceNowApplicationsItems = await serviceNowClient
            .GetFromJsonAsync<ServiceNowApplicationItemsJson>(_serviceNowOptions.ApplicationsEndpoint);
        ArgumentNullException.ThrowIfNull(serviceNowApplicationsItems);

        DbSet<ServiceNowApplication>? serviceNowApplicationsSet;

        try
        {
            serviceNowApplicationsSet = _context.Set<ServiceNowApplication>();
            var serviceNowApplications = serviceNowApplicationsItems.Result?.ToList();
            ArgumentNullException.ThrowIfNull(serviceNowApplications);

            foreach (var serviceNowApplication in serviceNowApplications)
            {
                var existingApplication = serviceNowApplicationsSet
                    .AsNoTracking()
                    .AsSplitQuery()
                    .SingleOrDefault(a => a.Number == serviceNowApplication.Number);
                
                if (existingApplication is not null)
                {
                    _logger.LogDebug("Service Now application with number '{Number}' already exists in the database", serviceNowApplication.Number);
                    _logger.LogInformation("Updating the existing Service Now application with number '{Number}'", serviceNowApplication.Number);
                    
                    existingApplication.MapFrom(serviceNowApplication);
                    serviceNowApplicationsSet.Update(existingApplication);

                    continue;
                }

                _logger.LogDebug("Service Now application with number '{Number}' does not exist in the database", serviceNowApplication.Number);
                _logger.LogInformation("Adding the new Service Now application with number '{Number}'", serviceNowApplication.Number);
                ServiceNowApplication newServiceNowApplication = new();
                newServiceNowApplication.MapFrom(serviceNowApplication);
                await serviceNowApplicationsSet.AddAsync(newServiceNowApplication);
            }

            serviceNowApplicationsSet
                    .AsNoTracking()
                    .AsSplitQuery()
                    .ToList()
                    .SyncRemovedConnections(serviceNowApplications);

            _logger.LogDebug("Saving changes to the database");
            await _context.SaveChangesAsync();
        }
        finally
        {
            _resourceStateService.IsServiceNowApplicationsAggregationRunning = false;
        }
        
        _resourceStateService.IsServiceNowApplicationsAggregationRunning = false;
        _logger.LogInformation("Completed aggregating Service Now applications from the configured Service Now SQL table instance");
        return serviceNowApplicationsSet.ToList().AsReadOnly();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ServiceNowApplication>> GetServiceNowApplicationsAsync(PaginationFilter paginationFilter)
    {
        ArgumentNullException.ThrowIfNull(paginationFilter);
        _logger.LogInformation("Getting Service Now applications from the configured Service Now SQL table instance");

        if (_resourceStateService.IsServiceNowApplicationsAggregationRunning || _resourceStateService.IsServiceNowApplicationsPurgingRunning)
        {
            _logger.LogWarning("Service Now applications aggregation or purging is already running");
            return Enumerable.Empty<ServiceNowApplication>();
        }
        
        var serviceNowApplicationsSet = _context.Set<ServiceNowApplication>();
        var serviceNowApplications = await serviceNowApplicationsSet
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(sa => sa.Number)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();
        ArgumentNullException.ThrowIfNull(serviceNowApplications);

        _logger.LogInformation("Completed getting Service Now applications from the configured Service Now SQL table instance");
        return serviceNowApplications.AsReadOnly();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ServiceNowApplication>> GetServiceNowApplicationsAsync()
    {
        _logger.LogInformation("Getting Service Now applications from the configured Service Now SQL table instance");

        if (_resourceStateService.IsServiceNowApplicationsAggregationRunning || _resourceStateService.IsServiceNowApplicationsPurgingRunning)
        {
            _logger.LogWarning("Service Now applications aggregation or purging is already running");
            return Enumerable.Empty<ServiceNowApplication>();
        }

        var serviceNowApplicationsSet = _context.Set<ServiceNowApplication>();
        var serviceNowApplications = await serviceNowApplicationsSet
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
        ArgumentNullException.ThrowIfNull(serviceNowApplications);

        _logger.LogInformation("Completed getting Service Now applications from the configured Service Now SQL table instance");
        return serviceNowApplications.AsReadOnly();
    }

    /// <inheritdoc/>
    public int GetServiceNowApplicationsCount() => _context.Set<ServiceNowApplication>().Count();

    /// <inheritdoc/>
    public async Task PurgeServiceNowApplicationsAsync()
    {
        _logger.LogInformation("Purging Service Now applications from the database");
        _resourceStateService.IsServiceNowApplicationsPurgingRunning = true;

        var serviceNowApplicationsSet = _context.Set<ServiceNowApplication>();
        serviceNowApplicationsSet.RemoveRange(serviceNowApplicationsSet);

        _logger.LogDebug("Saving changes to the database");
        await _context.SaveChangesAsync();
        _resourceStateService.IsServiceNowApplicationsPurgingRunning = false;
        _logger.LogInformation("Completed purging Service Now applications from the database");
    }

    private static bool IsNextPageAvailable(HttpResponseMessage httpResponseMessage)
    {
        var headers = httpResponseMessage.Headers;
        var links = headers.GetValues("Link").SingleOrDefault()?.ToString();
        
        if (string.IsNullOrEmpty(links))
        {
            return false;
        }

        return links.Contains("rel=\"next\"");
    }

    private static string GetNextPageLink(HttpResponseMessage response) =>
        response.Headers.TryGetValues("Link", out var links)
            ? links
                .SelectMany(l => l.Split(','))
                .Select(link => link.Split(';'))
                .Where(parts => parts.Length >= 2 && parts[1].Trim() is "rel=\"next\"")
                .Select(parts => parts[0].Trim('<', '>', ' '))
                .FirstOrDefault() ?? string.Empty
            : string.Empty;

    private async Task<IEnumerable<ServiceNowUser>> AggregateServiceNowUsersWithPaginatoinAsync(string pageLink)
    {
        // TODO: Refactor this code to use a more generic method for pagination
        _logger.LogInformation("Starting aggregation of service now users.");
        _resourceStateService.IsServiceNowUsersAggregationRunning = true;
        _logger.LogInformation("Creating a new HTTP client for interfacing with the Service Now API");
        var serviceNowHttpClient = _httpClientFactory.CreateClient(HttpClientNames.ServiceNowClient);
        var serviceNowUsersHttpResponse = await serviceNowHttpClient
            .GetAsync(pageLink);
        ArgumentNullException.ThrowIfNull(serviceNowUsersHttpResponse);
        var serviceNowUsersItemsJson = await serviceNowUsersHttpResponse.Content
            .ReadFromJsonAsync<ServiceNowUsersItemsJson>();
        ArgumentNullException.ThrowIfNull(serviceNowUsersItemsJson);

        var serviceNowUsersItemsJsonList = new List<ServiceNowUsersItemsJson>
        {
            serviceNowUsersItemsJson
        };

        if (IsNextPageAvailable(serviceNowUsersHttpResponse))
        {
            var isNextPageAvailable = true;
            while(isNextPageAvailable)
            {
                var nextPageLink = GetNextPageLink(serviceNowUsersHttpResponse);
                serviceNowUsersHttpResponse = await serviceNowHttpClient
                    .GetAsync(nextPageLink);
                ArgumentNullException.ThrowIfNull(serviceNowUsersHttpResponse);
                serviceNowUsersItemsJson = await serviceNowUsersHttpResponse.Content
                    .ReadFromJsonAsync<ServiceNowUsersItemsJson>();
                
                if (serviceNowUsersItemsJson is not null)
                {
                    serviceNowUsersItemsJsonList.Add(serviceNowUsersItemsJson);
                }

                isNextPageAvailable = IsNextPageAvailable(serviceNowUsersHttpResponse);
            }
        }
        
        ArgumentNullException.ThrowIfNull(serviceNowUsersItemsJson);
        ArgumentNullException.ThrowIfNull(serviceNowUsersItemsJsonList);

        DbSet<ServiceNowUser> serviceNowUsers = null!;
        
        foreach(var serviceNowUsersItemsJsonChunk in serviceNowUsersItemsJsonList)
        {
            try
            {
                serviceNowUsers = _context.Set<ServiceNowUser>();
                ArgumentNullException.ThrowIfNull(serviceNowUsers);

                var serviceNowUsersJson = serviceNowUsersItemsJsonChunk.Result?.ToList();
                ArgumentNullException.ThrowIfNull(serviceNowUsersJson);

                foreach(var user in serviceNowUsersJson)
                {
                    var existingUser = serviceNowUsers
                        .AsNoTracking()
                        .AsSplitQuery()
                        .SingleOrDefault(u => u.SysId == user.SysId);

                    if (existingUser is not null)
                    {
                        _logger.LogDebug("Service Now user with employee id '{EmployeeId}' already exists in the database", user.EmployeeNumber);
                        _logger.LogInformation("Updating the existing Service Now user with employee id '{EmployeeId}'", user.EmployeeNumber);

                        existingUser.MapFrom(user);
                        serviceNowUsers.Update(existingUser);

                        continue;
                    }

                    _logger.LogDebug("Service Now user with employee id '{EmployeeId}' does not exist in the database", user.EmployeeNumber);
                    _logger.LogInformation("Adding the new Service Now user with employee id '{EmployeeId}'", user.EmployeeNumber);
                    ServiceNowUser newServiceNowUser = new();
                    newServiceNowUser.MapFrom(user);
                    await serviceNowUsers.AddAsync(newServiceNowUser);
                }

                _logger.LogDebug("Saving changes to the database");
                await _context.SaveChangesAsync();
            }
            finally
            {
                _resourceStateService.IsServiceNowUsersAggregationRunning = false;
            }
        }

        _resourceStateService.IsServiceNowUsersAggregationRunning = false;
        _logger.LogInformation("Completed aggregation of service now users.");

        return serviceNowUsers.ToList().AsReadOnly();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ServiceNowUser>> AggregateServiceNowUsersAsync()
    {
        return await AggregateServiceNowUsersWithPaginatoinAsync(_serviceNowOptions.UsersEndpoint);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ServiceNowUser>> GetServiceNowUsersAsync()
    {
        _logger.LogInformation("Getting Service Now users from the configured Service Now SQL table instance");

        if (_resourceStateService.IsServiceNowUsersAggregationRunning || _resourceStateService.IsServiceNowUsersPurgingRunning)
        {
            _logger.LogWarning("Service Now users aggregation or purging is already running");
            return Enumerable.Empty<ServiceNowUser>();
        }

        var serviceNowUsersSet = _context.Set<ServiceNowUser>();
        var serviceNowUsers = await serviceNowUsersSet
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(snu => snu.EmployeeId)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(serviceNowUsers);

        _logger.LogInformation("Completed getting Service Now users from the configured Service Now SQL table instance");
        return serviceNowUsers.AsReadOnly();
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<ServiceNowUser>> GetServiceNowUsersAsync(PaginationFilter paginationFilter)
    {
        _logger.LogInformation("Getting Service Now users from the configured Service Now SQL table instance");

        if (_resourceStateService.IsServiceNowUsersAggregationRunning || _resourceStateService.IsServiceNowUsersPurgingRunning)
        {
            _logger.LogWarning("Service Now users aggregation or purging is already running");
            return Enumerable.Empty<ServiceNowUser>();
        }

        var serviceNowUsersSet = _context.Set<ServiceNowUser>();
        var serviceNowUsers = await serviceNowUsersSet
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(snu => snu.EmployeeId)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(serviceNowUsers);

        _logger.LogInformation("Completed getting Service Now users from the configured Service Now SQL table instance");
        return serviceNowUsers.AsReadOnly();
    }

    /// <inheritdoc/>
    public int GetServiceNowUsersCount() => _context.Set<ServiceNowUser>().Count();

    public async Task PurgeServiceNowUsersAsync()
    {
        _logger.LogInformation("Purging Service Now users from the database");
        _resourceStateService.IsServiceNowUsersPurgingRunning = true;

        var serviceNowUsersSet = _context.Set<ServiceNowUser>();
        serviceNowUsersSet.RemoveRange(serviceNowUsersSet);

        _logger.LogDebug("Saving changes to the database");
        await _context.SaveChangesAsync();
        _resourceStateService.IsServiceNowUsersPurgingRunning = false;
        _logger.LogInformation("Completed purging Service Now users from the database");
    }
}